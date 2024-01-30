namespace SpotifyAPIOAuth.Models
{
    //The recipient of the result
    //The Authorization guide implicitly states this in this passage https://developer.spotify.com/documentation/web-api/tutorials/client-credentials-flow
    public class AuthResult
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
