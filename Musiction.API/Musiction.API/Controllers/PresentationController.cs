using Microsoft.AspNetCore.Mvc;
using Musiction.API.IBusinessLogic;
using System.Collections.Generic;

namespace Musiction.API.Controllers
{
    [Route("api/presentation")]
    public class PresentationController : Controller
    {
        private readonly ICreatePresentationResponse _presentationResponse;

        public PresentationController(ICreatePresentationResponse presentationResponse)
        {
            _presentationResponse = presentationResponse;
        }


        [HttpGet("{returnLinkTo}")]
        public IActionResult Presentation(string returnLinkTo, [FromQuery]List<int> ids)
        {
            if (returnLinkTo == "pptx")
            {
                var response = _presentationResponse.CreatePptxResponse(ids);
                return Ok(response);
            }
            else if (returnLinkTo == "zip")
            {
                var response = _presentationResponse.CreateZipResponse(ids);
                return Ok(response);
            }

            return BadRequest();
        }
    }
}
