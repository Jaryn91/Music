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
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
