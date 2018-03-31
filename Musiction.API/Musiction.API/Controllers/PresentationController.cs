﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musiction.API.BusinessLogic;
using Musiction.API.Services;
using System.Collections.Generic;

namespace Musiction.API.Controllers
{
    [Route("api/presentation")]
    public class PresentationController : Controller
    {
        private ILogger<SongsController> _logger;
        private IMailService _mailService;
        private ISongRepository _songRepository;

        public PresentationController(ILogger<SongsController> logger, IMailService mailService, ISongRepository songRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _songRepository = songRepository;
        }

        [HttpGet("{returnLinkTo}")]

        public IActionResult Presentation(string returnLinkTo, [FromQuery]List<int> ids)
        {
            var songs = _songRepository.GetSongsInOrder(ids);

            var paths = new List<string>();
            foreach (var song in songs)
            {
                paths.Add(song.Path);
            }

            var merger = new PowerPointMerger();
            var pathToCombinedPptx = merger.Merge(paths);

            if (returnLinkTo == "pptx")
                return Ok(pathToCombinedPptx);
            else if (returnLinkTo == "zip")
            {
                var pptxConverter = new PptxToJpgConverter();
                var pathToZip = pptxConverter.Convert(pathToCombinedPptx);
                return Ok(pathToZip);
            }

            return BadRequest();


        }
    }
}
