using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using authentication.Services.OauthLoginServices;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Linq;
using OAuthTest.Models.Configs;
using OAuthTest.Models;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace OAuthTest.Controllers
{

    [Route("authentication/login")]
    [EnableCors("AloowCors")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOAuthLoginService _OAuthLoginService;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [AllowAnonymous]
        [EnableCors("fully permissive")]
        [HttpGet("signingoogle")]
        public IActionResult signInWithGoogle()
        {
            var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
            Url.Action(nameof(HandleExternalLogin)));
            return Challenge(authenticationProperties, "Google");

        }
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded) //user does not exist yet
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

                await _userManager.AddLoginAsync(newUser, info);
                var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id));
                await _userManager.AddClaimsAsync(newUser, newUserClaims);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            return Redirect("http://localhost:5000/api/home/name");
        }


        [Route("logout")]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("http://localhost:5000/api/teste");
        }


    }
}