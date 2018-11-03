using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musiction.API.IBusinessLogic;

namespace Musiction.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }

        [HttpGet]
        [Route("api/account/credits")]
        public IActionResult GetRemainingCredits()
        {
            var remainingCredits = _account.GetRemainingCredits();
            return Ok(remainingCredits);
        }

        [HttpGet, Authorize]
        [Route("api/account/testAuth")]
        public IActionResult TestAuth()
        {
            return Ok();
        }
    }
}
