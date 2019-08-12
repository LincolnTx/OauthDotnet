
namespace OAuthTest.Models.Configs
{
    public class GoogleOAuthConfig
    {
        public string GoogleClientId { set; get; }
        public string GoogleClientSecret { set; get; }
        public string GoogleUrl { set; get; }
        public string GoogleJwtEmailEncryptation { set; get; }
        public string GoogleJwtSecret { set; get; }
        public string CallbackUrl { set; get; }
        public string GoogleApplicationName { set; get; }
    }
}