using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Content("Running...");
        }


        [Route("api/teste")]
        public string teste()
        {
            return "teste";
        }
        [Route("api/home/isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            return new ObjectResult(User.Identity.IsAuthenticated);
        }

        [Route("api/home/fail")]
        public IActionResult Fail()
        {
            return Unauthorized();
        }

        [AllowAnonymous]
        [Authorize]
        [Route("api/home/name")]
        public IActionResult Name()
        {
            var claimsPrincipal = (ClaimsPrincipal)User;
            var givenName = claimsPrincipal.FindFirst(ClaimTypes.GivenName).Value;
            var giveUname = claimsPrincipal.FindFirst(ClaimTypes.Surname).Value;
            var givenEmail = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
            return Ok($"Ola {givenName} {giveUname} o seu email é : {givenEmail}");

        }

        [Route("home/[action]")]
        public IActionResult Denied()
        {
            return Content("You need to allow this application access in Google order to be able to login");
        }
    }
}