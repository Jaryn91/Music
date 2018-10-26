using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private IOutcomeTextCreator _outcomeTextCreator;

        public PresentationController(ILogger<SongsController> logger, IMailService mailService,
            ISongRepository songRepository, IFileAndFolderPathsCreator fileAndFolderPath,
            IOutcomeTextCreator outcomeTextCreator)
        {
            _logger = logger;
            _mailService = mailService;
            _songRepository = songRepository;
            _fileAndFolderPath = fileAndFolderPath;
            _outcomeTextCreator = outcomeTextCreator;
        }


        [HttpGet("{returnLinkTo}")]

        public IActionResult Presentation(string returnLinkTo, [FromQuery]List<int> ids)
        {
            var presentationResponse = new PresentationResponse(_fileAndFolderPath, _outcomeTextCreator, _songRepository);

            if (returnLinkTo == "pptx")
            {
                var response = presentationResponse.CreatePptxResponse(ids);
                return Ok(response);
            }
            else if (returnLinkTo == "zip")
            {
                var response = presentationResponse.CreateZipResponse(ids);
                return Ok(response);
            }

            return BadRequest();
        }
    }
}
