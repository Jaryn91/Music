using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using Musiction.API.Models;
using Musiction.API.Resources;
using Musiction.API.Services;
using System;
using System.Collections.Generic;

namespace Musiction.API.Controllers
{
    [Route("api/songs")]
    public class SongsController : Controller
    {

        private readonly ILogger<SongsController> _logger;
        private readonly ISongRepository _songRepository;
        private readonly IGoogleSlides _googleSlides;

        public SongsController(ILogger<SongsController> logger,
            ISongRepository songRepository, IGoogleSlides googleSlides)
        {
            _logger = logger;
            _songRepository = songRepository;
            _googleSlides = googleSlides;
        }

        [HttpGet, Authorize]
        public IActionResult GetSongs()
        {
            var songResponse = new SongResponse();
            try
            {
                var songs = _songRepository.GetSongs();
                var results = Mapper.Map<IEnumerable<SongDto>>(songs);
                songResponse.Songs = results;
                return Ok(songResponse);
            }
            catch (Exception ex)
            {
                songResponse.AlertMessage = ex.Message;
                return BadRequest(songResponse);
            }
        }

        [HttpGet("{id}", Name = "GetSong"), Authorize]
        public IActionResult GetSong(int id)
        {
            var songResponse = new SongResponse();
            try
            {
                var songToReturn = _songRepository.GetSong(id);
                if (songToReturn == null)
                {
                    songResponse.AlertMessage = string.Format(MagicString.SongWithIdDoesntExist, id);
                    return BadRequest(songResponse);
                }

                var song = Mapper.Map<SongDto>(songToReturn);
                songResponse.Songs = new List<SongDto>() { song };
                return Ok(songResponse);
            }
            catch (Exception ex)
            {
                songResponse.AlertMessage = ex.Message;
                return BadRequest(songResponse);
            }
        }


        [HttpPost("{songName}"), Authorize]
        public IActionResult CreateSong(string songName)
        {
            if (songName == "")
                return BadRequest();

            var presentationId = "";
            var songResponse = new SongResponse();
            try
            {
                presentationId = _googleSlides.Create(songName);

                var song = new Song() { Name = songName, PresentationId = presentationId };

                if (!_songRepository.AddSong(song))
                {
                    _googleSlides.Remove(presentationId);
                    songResponse.AlertMessage = MagicString.ProblemOucuredDuringSavingSongToDatabase;
                    return BadRequest(songResponse);
                }

                var createdSong = Mapper.Map<SongDto>(song);

                return Ok(createdSong);
            }
            catch (Exception ex)
            {
                _googleSlides.Remove(presentationId);
                songResponse.AlertMessage = ex.Message;
                return BadRequest(songResponse);
            }
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult UpdateSong(int id, SongForUpdateDto song)
        {
            if (song == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var songResponse = new SongResponse();
            try
            {
                var songToUpdate = _songRepository.GetSong(id);

                if (songToUpdate == null)
                {
                    songResponse.AlertMessage = string.Format(MagicString.SongWithIdDoesntExist, id);
                    return BadRequest(songResponse);
                }

                Mapper.Map(song, songToUpdate);

                if (!_songRepository.Save())
                {
                    songResponse.AlertMessage = MagicString.ProblemOucuredDuringSavingSongToDatabase;
                    return BadRequest(songResponse);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                songResponse.AlertMessage = ex.Message;
                return BadRequest(songResponse);
            }
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult DeleteSong(int id)
        {
            var songResponse = new SongResponse();
            try
            {
                var songToDelete = _songRepository.GetSong(id);

                if (songToDelete == null)
                {
                    songResponse.AlertMessage = string.Format(MagicString.SongWithIdDoesntExist, id);
                    return BadRequest(songResponse);
                }

                _songRepository.RemoveSong(songToDelete);
                if (!_songRepository.Save())
                {
                    songResponse.AlertMessage = MagicString.ProblemOucuredDuringSavingSongToDatabase;
                    return BadRequest(songResponse);
                }

                _googleSlides.Remove(songToDelete.PresentationId);

                return Ok();
            }
            catch (Exception ex)
            {
                songResponse.AlertMessage = ex.Message;
                return BadRequest(songResponse);
            }
        }
    }
}
