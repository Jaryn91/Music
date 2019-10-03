using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
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
using System.Linq;

namespace Musiction.API.Controllers
{
    [Route("api/songs")]
    public class SongsController : Controller
    {

        private readonly ILogger<SongsController> _logger;
        private readonly ISongRepository _songRepository;
        private readonly IGoogleSlides _googleSlides;
        private readonly IGetValue _valueRetrieval;

        public SongsController(ILogger<SongsController> logger,
            ISongRepository songRepository, IGoogleSlides googleSlides,
            IGetValue valueRetrieval)
        {
            _logger = logger;
            _songRepository = songRepository;
            _googleSlides = googleSlides;
            _valueRetrieval = valueRetrieval;
        }

        [HttpGet]
        public IActionResult GetSongs()
        {
            var songResponse = new SongResponse();
            try
            {
                var songs = _songRepository.Get();
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

        [HttpGet("{id}", Name = "Get"), Authorize]
        public IActionResult GetSong(int id)
        {
            var songResponse = new SongResponse();
            try
            {
                var songToReturn = _songRepository.Get(id);
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

                if (!_songRepository.Add(song))
                {
                    _googleSlides.Remove(presentationId);
                    songResponse.AlertMessage = MagicString.ProblemOucuredDuringSavingSongToDatabase;
                    return BadRequest(songResponse);
                }

                var createdSong = Mapper.Map<SongDto>(song);

                var historyEntity = new History()
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = GetUserInformation().FullName,
                    Information = $"Dodano nową piosnkę z Id: {createdSong.Id} o tytule {createdSong.Name}"
                };


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
                var songToUpdate = _songRepository.Get(id);

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
                var songToDelete = _songRepository.Get(id);

                if (songToDelete == null)
                {
                    songResponse.AlertMessage = string.Format(MagicString.SongWithIdDoesntExist, id);
                    return BadRequest(songResponse);
                }

                _songRepository.Remove(songToDelete);
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



        private UserInfo GetUserInformation()
        {
            // Retrieve the access_token claim which we saved in the OnTokenValidated event
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

            // If we have an access_token, then retrieve the user's information
            if (string.IsNullOrEmpty(accessToken)) return null;

            var apiClient = new AuthenticationApiClient(_valueRetrieval.Get("Auth0:Domain"));
            var userInfo = apiClient.GetUserInfoAsync(accessToken);
            userInfo.Wait();
            var user = userInfo.Result;
            return user;
        }
    }
}
