using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using AutoMapper;
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
        private readonly IGoogleSlides _googleSlides;


        public PresentationController(ISongRepository songRepository,
            IConvertPresentation pptxToZipConverter, IMerge powerPointMerger,
            IFileAndFolderPathsCreator fileAndFolderPath, IPresentationRepository presentationRepository,
            IGetValue valueRetrieval, IGoogleSlides googleSlides)
        {
            _powerPointMerger = powerPointMerger;
            _songRepository = songRepository;
            _pptxToZipConverter = pptxToZipConverter;
            _fileAndFolderPath = fileAndFolderPath;
            _presentationRepository = presentationRepository;
            _valueRetrieval = valueRetrieval;
            _googleSlides = googleSlides;
        }


        [HttpGet("{presentationType}"), Authorize]
        public IActionResult Presentation(string presentationType, [FromQuery]List<int> ids)
        {
            IEnumerable<Song> songs = new List<Song>();
            try
            {
                songs = _songRepository.GetSongsInOrder(ids);
                if (!songs.Any())
                    return BadRequest(NoSongResponse(songs));

                var zippedPresentationId = "";
                var zippedPresentationOnLocalhost = "";
                var mergedPptxPresentationOnLocalhost = _powerPointMerger.Merge(songs);
                var mergedPresentationId = _googleSlides.AddPptxFile(mergedPptxPresentationOnLocalhost);


                if (presentationType == "zip")
                {
                    zippedPresentationOnLocalhost = _pptxToZipConverter.Convert(mergedPptxPresentationOnLocalhost);
                    zippedPresentationId = _googleSlides.AddZipFile(zippedPresentationOnLocalhost);
                    System.IO.File.Delete(zippedPresentationOnLocalhost);
                }

                var response = CreateResponseAndHistoryLog(mergedPresentationId, zippedPresentationId, presentationType, songs);

                System.IO.File.Delete(mergedPresentationId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var presentationResponse = new PresentationResponse();
                presentationResponse.CreateExceptionResponse(songs, ex.Message);
                return BadRequest(presentationResponse);
            }
        }

        private PresentationResponse NoSongResponse(IEnumerable<Song> songs)
        {
            var presentationResponse = new PresentationResponse();
            presentationResponse.CreateExceptionResponse(songs, MagicString.NoSongSelected);
            return presentationResponse;
        }

        private PresentationResponse CreateResponseAndHistoryLog(string mergedPresentationId, string zippedPresentationId, string presentationType, IEnumerable<Song> songs)
        {
            var presentationResponse = new PresentationResponse();
            CreatePresentationWithLinksToSongs(mergedPresentationId, zippedPresentationId, presentationType, songs);
            presentationResponse.CreateSuccessResponse(mergedPresentationId, zippedPresentationId, presentationType, songs);
            return presentationResponse;
        }

        [HttpGet]
        public IActionResult GetPresentations()
        {
            try
            {
                var presentations = _presentationRepository.Get().ToList();
                var presentationDto = Mapper.Map<IEnumerable<PresentationDto>>(presentations).ToList();

                for (var i = 0; i < presentations.Count; i++)
                {
                    presentationDto[i].UrlToPptx = String.Format(MagicString.PathToDownloadFileFromGoogleDrive, presentations[i].GoogleDrivePptxFileId);
                    if (presentations[i].GoogleDriveZipFileId != null)
                        presentationDto[i].UrlToZip = String.Format(MagicString.PathToDownloadFileFromGoogleDrive, presentations[i].GoogleDriveZipFileId);
                }

                presentationDto.Reverse();
                return Ok(presentationDto);
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }



        private void CreatePresentationWithLinksToSongs(string googleDriveFileId, string zippedPresentationId, string presentationType, IEnumerable<Song> songs)
        {
            var presentation = new Presentation(googleDriveFileId, zippedPresentationId, presentationType, GetUserInformation());

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
