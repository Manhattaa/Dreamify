namespace SpotifyAPIOAuth.Services
{
    public interface ISpotifyAccountService
    {
        //Calling GetToken Asynchronously the string within the Task is the AccessToken
            Task<string> GetToken(string clientId, string clientSecret);
        }
    }
