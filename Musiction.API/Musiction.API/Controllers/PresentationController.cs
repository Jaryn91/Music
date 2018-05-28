﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musiction.API.BusinessLogic;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
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
        private IFileAndFolderPathsCreator _fileAndFolderPath;

        public PresentationController(ILogger<SongsController> logger, IMailService mailService, ISongRepository songRepository, IFileAndFolderPathsCreator fileAndFolderPath)
        {
            _logger = logger;
            _mailService = mailService;
            _songRepository = songRepository;
            _fileAndFolderPath = fileAndFolderPath;
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

            var merger = new PowerPointMerger(_fileAndFolderPath);
            var pathToCombinedPptx = merger.Merge(paths);

            var presentationResponse = new PresentationResponse();

            if (returnLinkTo == "pptx")
            {
                string webAddess = _fileAndFolderPath.GetWebAddressToFile(pathToCombinedPptx);
                return Ok(webAddess);
            }
            else if (returnLinkTo == "zip")
            {
                var pptxConverter = new PptxToJpgConverter(_fileAndFolderPath);
                var pathToZip = pptxConverter.Convert(pathToCombinedPptx);
                string webAddess = _fileAndFolderPath.GetWebAddressToFile(pathToZip);
                return Ok(webAddess);
            }

            return BadRequest();
        }
    }
}
