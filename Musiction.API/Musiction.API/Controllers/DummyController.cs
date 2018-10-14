using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musiction.API.Entities;

namespace Musiction.API.Controllers
{
    public class DummyController : Controller
    {
        private SongContext _ctx;

        public DummyController(SongContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Authorize]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("api/testAuth")]
        public IActionResult TestAuth()
        {
            return Ok();
        }
    }
}
