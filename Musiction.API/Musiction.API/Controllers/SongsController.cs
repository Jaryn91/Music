using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musiction.API.Models;
using Musiction.API.Services;
using System;
using System.Linq;

namespace Musiction.API.Controllers
{
    [Route("api/songs")]
    public class SongsController : Controller
    {

        private ILogger<SongsController> _logger;
        private IMailService _mailService;

        public SongsController(ILogger<SongsController> logger,
            IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet()]
        public IActionResult GetSongs()
        {
            return Ok(SongsDataStore.Current.Songs);
        }

        [HttpGet("{id}", Name = "GetSong")]
        public IActionResult GetSong(int id)
        {
            try
            {
                var songToReturn = SongsDataStore.Current.Songs.FirstOrDefault(s => s.Id == id);
                if (songToReturn == null)
                {
                    _logger.LogInformation($"Song {id} is not found");
                    return NotFound();
                }

                return Ok(songToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Excepction occoured while looking for song with {id}");
                return StatusCode(500, "A problem happend on GetSong");
            }
        }

        [HttpPost()]
        public IActionResult CreateSong([FromBody] SongCreationDto song)
        {
            if (song == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var maxSongId = SongsDataStore.Current.Songs.Max(p => p.Id);
            var finalSong = new SongDto()
            {
                Id = ++maxSongId,
                Name = song.Name,
                Path = song.Path
            };

            SongsDataStore.Current.Songs.Add(finalSong);

            return CreatedAtRoute("GetSong", new { id = maxSongId }, finalSong);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, [FromBody] SongForUpdateDto song)
        {
            if (song == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var maxSongId = SongsDataStore.Current.Songs.Max(p => p.Id);

            var songToUpdate = SongsDataStore.Current.Songs.FirstOrDefault(s => s.Id == id);

            if (songToUpdate == null)
                return NotFound();

            songToUpdate.Name = song.Name;
            songToUpdate.Path = song.Path;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateSong(int id,
            [FromBody] JsonPatchDocument<SongForUpdateDto> patchSong)
        {
            if (patchSong == null)
                return BadRequest();


            var songFromStore = SongsDataStore.Current.Songs.FirstOrDefault(s => s.Id == id);
            if (songFromStore == null)
                return NotFound();

            var songToPatch = new SongForUpdateDto()
            {
                Name = songFromStore.Name,
                Path = songFromStore.Path
            };

            patchSong.ApplyTo(songToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TryValidateModel(songToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            songFromStore.Name = songToPatch.Name;
            songFromStore.Path = songToPatch.Path;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var songToDelete = SongsDataStore.Current.Songs.FirstOrDefault(s => s.Id == id);

            if (songToDelete == null)
                return NotFound();

            SongsDataStore.Current.Songs.Remove(songToDelete);

            _mailService.Send("Song is removed", $"Song with id {id} has been removed");

            return NoContent();
        }
    }
}
