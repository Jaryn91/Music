using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/presentation")]
    public class PresentationController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IMerge _powerPointMerger;
        private readonly IConvertPresentation _pptxToZipConverter;
        private readonly IFileAndFolderPathsCreator _fileAndFolderPath;
        private readonly IPresentationRepository _presentationRepository;
        private readonly IGetValue _valueRetrieval;

        public PresentationController(ISongRepository songRepository,
            IConvertPresentation pptxToZipConverter, IMerge powerPointMerger,
            IFileAndFolderPathsCreator fileAndFolderPath, IPresentationRepository presentationRepository,
            IGetValue valueRetrieval)
        {
            _powerPointMerger = powerPointMerger;
            _songRepository = songRepository;
            _pptxToZipConverter = pptxToZipConverter;
            _fileAndFolderPath = fileAndFolderPath;
            _presentationRepository = presentationRepository;
            _valueRetrieval = valueRetrieval;
        }


        [HttpGet("{presentationType}"), Authorize]
        public IActionResult Presentation(string presentationType, [FromQuery]List<int> ids)
        {
            var presentationResponse = new PresentationResponse();
            IEnumerable<Song> songs = new List<Song>();
            try
            {
                songs = _songRepository.GetSongsInOrder(ids);
                if (!songs.Any())
                {
                    presentationResponse.CreateExceptionResponse(songs, MagicString.NoSongSelected);
                    return BadRequest(presentationResponse);
                }

                var finalPptxFileName = _powerPointMerger.Merge(songs);

                if (presentationType == "pptx")
                {
                    var urlToMergedPresentation = _fileAndFolderPath.GetUrlToFile(finalPptxFileName);
                    presentationResponse.CreateSuccessResponse(songs, urlToMergedPresentation);

                    CreatePresentationWithLinksToSongs(finalPptxFileName, songs, presentationType);

                    return Ok(presentationResponse);
                }

                if (presentationType == "zip")
                {
                    var pathToFinalPptxFile = _fileAndFolderPath.GetPathToMergedFiles(finalPptxFileName);
                    var pathToZip = _pptxToZipConverter.Convert(pathToFinalPptxFile);
                    var urlToZip = _fileAndFolderPath.GetUrlToFile(pathToZip);

                    CreatePresentationWithLinksToSongs(finalPptxFileName, songs, presentationType);

                    presentationResponse.CreateSuccessResponse(songs, urlToZip);
                    return Ok(presentationResponse);
                }
            }
            catch (Exception ex)
            {
                presentationResponse.CreateExceptionResponse(songs, ex.Message);
                return BadRequest(presentationResponse);
            }
            return BadRequest();
        }

        private void CreatePresentationWithLinksToSongs(string finalFileName, IEnumerable<Song> songs, string returnLinkTo)
        {
            if (returnLinkTo == "zip")
                finalFileName = finalFileName.Replace("pptx", "zip");
            var presentation = new Presentation(finalFileName, GetUserInformation());

            var list = new List<LinkSongToPresentation>();

            foreach (var song in songs)
            {
                var link = new LinkSongToPresentation() { Presentation = presentation, Song = song };
                list.Add(link);
                song.LinkSongToPresentation.Add(link);
            }

            presentation.LinkSongToPresentation.AddRange(list);
            _presentationRepository.Save();
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
