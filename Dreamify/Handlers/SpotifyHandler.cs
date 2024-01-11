namespace Dreamify.Handlers
{
    public class SpotifyHandler
    {
        public interface ISpotifyHandler
        {
            Task<string> GetAccessToken();
        }
        public class SpotifyHandler : ISpotifyHandler
        {
            private string _clientId;
            private string _clientSecret;
            //parameter logic
            //parameter logic
            
            //method for GetAccessToken

            //httpclient logic

           
        }
    }
}
