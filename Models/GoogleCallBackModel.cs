
namespace OAuthTest.Models
{
    public class GoogleCallBackModel
    {
        public string state { set; get; }
        public string code { set; get; }
        public string scope { set; get; }
        public string authUser { set; get; }
        public string sessionState { set; get; }
        public string prompt { set; get; }
    }
}