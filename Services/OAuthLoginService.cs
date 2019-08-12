using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;
using System.Linq;

using authentication.Services.OauthLoginServices;
using System.Security.Cryptography;

namespace OAuthTest.Services.OauthLoginServices
{
    public class OAuthLoginServices : IOAuthLoginService
    {
        #region members
        private readonly UserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region  constructor
        public OAuthLoginServices(
            UserService userService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
            this._userService = userService;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        #endregion

        public async Task<string> VerifyOauthLogin(string email)
        {

            return "ok";
        }



    }
}