using Microsoft.AspNetCore.Mvc;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
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

        public PresentationController(ISongRepository songRepository,
            IConvertPresentation pptxToZipConverter, IMerge powerPointMerger,
            IFileAndFolderPathsCreator fileAndFolderPath, IPresentationRepository presentationRepository)
        {
            _powerPointMerger = powerPointMerger;
            _songRepository = songRepository;
            _pptxToZipConverter = pptxToZipConverter;
            _fileAndFolderPath = fileAndFolderPath;
            _presentationRepository = presentationRepository;
        }


        [HttpGet("{returnLinkTo}")]
        public IActionResult Presentation(string returnLinkTo, [FromQuery]List<int> ids)
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

                var pathToMergedPresentation = _powerPointMerger.Merge(songs);

                if (returnLinkTo == "pptx")
                {
                    var urlToMergedPresentation = _fileAndFolderPath.GetUrlToFile(pathToMergedPresentation);
                    presentationResponse.CreateSuccessResponse(songs, urlToMergedPresentation);
                    var presentation = new Presentation
                    {
                        CreateBy = "test",
                        CreatedDate = DateTime.Now,
                        Path = urlToMergedPresentation,
                        Type = "pptx"
                    };

                    List<LinkSongToPresentation> list = new List<LinkSongToPresentation>();

                    foreach (var song in songs)
                    {
                        var link = new LinkSongToPresentation() { Presentation = presentation, Song = song };
                        list.Add(link);
                        song.LinkSongToPresentation.Add(link);
                    }

                    presentation.LinkSongToPresentation.AddRange(list);

                    var added = _presentationRepository.Save();
                    return Ok(presentationResponse);
                }

                if (returnLinkTo == "zip")
                {
                    var pathToZip = _pptxToZipConverter.Convert(pathToMergedPresentation);
                    var urlToZip = _fileAndFolderPath.GetUrlToFile(pathToZip);
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
    }
}
