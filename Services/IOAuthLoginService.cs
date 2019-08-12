using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace authentication.Services.OauthLoginServices
{
    public interface IOAuthLoginService
    {
        Task<string> VerifyOauthLogin(string email);

    }


}