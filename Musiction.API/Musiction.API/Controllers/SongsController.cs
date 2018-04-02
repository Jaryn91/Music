using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musiction.API.BusinessLogic;
using Musiction.API.Entities;
using Musiction.API.Models;
using Musiction.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Musiction.API.Controllers
{
    [Route("api/songs")]
    public class SongsController : Controller
    {

        private ILogger<SongsController> _logger;
        private IMailService _mailService;
        private ISongRepository _songRepository;
        private IFileAndFolderPath _fileAndFolderPath;

        public SongsController(ILogger<SongsController> logger,
            IMailService mailService, ISongRepository songRepository, IFileAndFolderPath fileAndFolderPath)
        {
            _logger = logger;
            _mailService = mailService;
            _songRepository = songRepository;
            _fileAndFolderPath = fileAndFolderPath;
        }

        [HttpGet()]
        public IActionResult GetSongs()
        {
            var songs = _songRepository.GetSongs();
            var results = Mapper.Map<IEnumerable<SongDto>>(songs);
            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetSong")]
        public IActionResult GetSong(int id)
        {
            try
            {
                var songToReturn = _songRepository.GetSong(id);
                if (songToReturn == null)
                {
                    _logger.LogInformation($"Song {id} is not found");
                    return NotFound();
                }

                var song = Mapper.Map<SongDto>(songToReturn);
                return Ok(song);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Excepction occoured while looking for song with {id}");
                return StatusCode(500, "A problem happend on GetSong");
            }
        }


        [HttpPost()]
        public async Task<IActionResult> CreateSong(SongForCreationDto song)
        {

            if (song == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filePath = _fileAndFolderPath.GetPresentationFilePath(song.PptxFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await song.PptxFile.CopyToAsync(fileStream);
            }

            var finalSong = Mapper.Map<Song>(song);
            finalSong.Path = filePath;

            if (!_songRepository.AddSong(finalSong))
            {
                return StatusCode(500, "A problem happend durning saving a song");
            }

            var createdSong = Mapper.Map<SongDto>(finalSong);

            return CreatedAtRoute("GetSong", new { id = createdSong.Id }, createdSong);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, SongForUpdateDto song)
        {
            if (song == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //ifSongisUpdatedThenRemoveOldOneAndAddNewOne

            var songToUpdate = _songRepository.GetSong(id);

            if (songToUpdate == null)
                return NotFound();

            Mapper.Map(song, songToUpdate);

            if (!_songRepository.Save())
            {
                return StatusCode(500, "A problem happend durning updating a song");
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var songToDelete = _songRepository.GetSong(id);

            if (songToDelete == null)
                return NotFound();

            _songRepository.RemoveSong(songToDelete);


            if (!_songRepository.Save())
            {
                return StatusCode(500, "A problem happend durning deleting a song");
            }

            return NoContent();
        }
    }
}
