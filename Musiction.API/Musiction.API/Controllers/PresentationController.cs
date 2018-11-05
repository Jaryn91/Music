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

        public PresentationController(ISongRepository songRepository,
            IConvertPresentation pptxToZipConverter, IMerge powerPointMerger)
        {
            _powerPointMerger = powerPointMerger;
            _songRepository = songRepository;
            _pptxToZipConverter = pptxToZipConverter;
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

                var urlToMergedPresentations = _powerPointMerger.Merge(songs, true);

                if (returnLinkTo == "pptx")
                {
                    presentationResponse.CreateSuccessResponse(songs, urlToMergedPresentations);
                    return Ok(presentationResponse);
                }

                if (returnLinkTo == "zip")
                {
                    var urlToZip = _pptxToZipConverter.Convert(urlToMergedPresentations);
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
