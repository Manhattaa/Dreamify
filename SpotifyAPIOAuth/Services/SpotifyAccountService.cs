using SpotifyAPIOAuth.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace SpotifyAPIOAuth.Services
{
    public class SpotifyAccountService : ISpotifyAccountService
    {
        private readonly HttpClient _httpClient;

        public SpotifyAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //According to Spotify's directives you need to set up an HTTP Pass request to "POST https://accounts.spotify.com/api/token".
        //So we need to use the endpoint ~/token and also have it take in our trusty ClientID and ClientSecret Passcodes.
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            //HttpRequestMessage uses the argument HttpMethod and its a POST, and we finish with the end of the link above and just call it "token" to specify it
            var request = new HttpRequestMessage(HttpMethod.Post, "token");

            //Here we need to set up an Authorization parameter as also provided by Spotify. "Basic <base64 encoded client_id:client_secret>"
            request.Headers.Authorization = new AuthenticationHeaderValue(
                //This string combines the clientId and clientSecret and encodes it in base64
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            //One also needs to use create a body as a FormURLEncoded. The Request Body parameter "grant-type" needs to be set as client_credentials.
            //this
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });

            var response = await _httpClient.SendAsync(request);
            //This makes sure the code succeeds otherwise we get an exception
            response.EnsureSuccessStatusCode();

            //We need to read the response. it gets returned as a Stream (https://learn.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-8.0)
            //We then deserialize it to get an object that we can actually use.
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;
        }
    }
}