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


        [HttpGet("pptx/"), Authorize]
        public IActionResult Presentation([FromQuery]List<int> ids)
        {
            IEnumerable<Song> songs = new List<Song>();
            try
            {
                songs = _songRepository.GetSongsInOrder(ids);
                if (!songs.Any())
                    return BadRequest(NoSongResponse(songs));

                var mergedPptxPresentationOnLocalhost = _powerPointMerger.Merge(songs);
                var mergedPresentation = _googleSlides.AddPptxFile(mergedPptxPresentationOnLocalhost);

                var response = CreateResponseForPptxAndHistoryLog(mergedPresentation, songs);

                System.IO.File.Delete(mergedPptxPresentationOnLocalhost);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var presentationResponse = new PresentationResponse();
                presentationResponse.CreateExceptionResponse(songs, ex.Message);
                return BadRequest(presentationResponse);
            }
        }

        [HttpGet("zip/{googleDriveFileId}"), Authorize]
        public IActionResult Presentation(string googleDriveFileId)
        {
            try
            {
                var presentation = _presentationRepository.Get(googleDriveFileId);
                var pptxMergedFileOnLocalhost = _googleSlides.DownloadPptx(presentation, googleDriveFileId);

                var zippedPresentationOnLocalhost = _pptxToZipConverter.Convert(pptxMergedFileOnLocalhost);
                var zippedPresentation = _googleSlides.AddZipFile(zippedPresentationOnLocalhost);

                var response = CreateResponseAndHistoryLog(zippedPresentation, presentation);

                System.IO.File.Delete(zippedPresentationOnLocalhost);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var presentationResponse = new PresentationResponse();
                presentationResponse.CreateExceptionResponse(null, ex.Message);
                return BadRequest(presentationResponse);
            }
        }

        private PresentationResponse CreateResponseAndHistoryLog(PresentationOnDrive zippedPresentation, Presentation presentation)
        {
            var presentationResponse = new PresentationResponse();
            if (zippedPresentation.Extension == "Zip")
            {
                presentation.GoogleDriveZipFileId = zippedPresentation.FileId;
                _presentationRepository.Save();
            }

            var presentationDto = Mapper.Map<PresentationDto>(presentation);
            presentationResponse.CreateSuccessResponse(presentationDto);
            return presentationResponse;
        }

        private PresentationResponse CreateResponseForPptxAndHistoryLog(PresentationOnDrive presentationOnDrive, IEnumerable<Song> songs)
        {
            var presentationResponse = new PresentationResponse();
            var presentation = CreatePresentationWithLinksToSongs(presentationOnDrive, songs);

            var presentationDto = Mapper.Map<PresentationDto>(presentation);
            presentationResponse.CreateSuccessResponse(presentationDto);
            return presentationResponse;
        }

        private PresentationResponse NoSongResponse(IEnumerable<Song> songs)
        {
            var presentationResponse = new PresentationResponse();
            presentationResponse.CreateExceptionResponse(songs, MagicString.NoSongSelected);
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



        private Presentation CreatePresentationWithLinksToSongs(PresentationOnDrive presentationOnDrive, IEnumerable<Song> songs)
        {
            var presentation = new Presentation(presentationOnDrive.FileId, GetUserInformation());

            var list = new List<LinkSongToPresentation>();

            foreach (var song in songs)
            {
                var link = new LinkSongToPresentation() { Presentation = presentation, Song = song };
                list.Add(link);
                song.LinkSongToPresentation.Add(link);
            }

            presentation.LinkSongToPresentation.AddRange(list);
            _presentationRepository.Save();
            return presentation;
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
