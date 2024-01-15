using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dreamify.Models.Dtos;

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
            private HttpClient _httpClient;
            private DateTime _hourlyToken;
            private string _accessToken;

            public SpotifyHandler(string clientId, string clientSecret) : this(new HttpClient(), clientId, clientSecret)
            {

            }

            public SpotifyHandler(string clientId, string clientSecret, HttpClient httpClient)
            {
                _httpClient = httpClient;
                _clientId = clientId;
                _clientSecret = clientSecret;
            }



            public async Task<string> GetAccessToken()
            {
                var hourlyUpdate = (DateTime.Now - _hourlyToken).TotalMinutes; //https://learn.microsoft.com/en-us/dotnet/api/system.timespan.totalminutes?view=net-8.0

                // https://developer.spotify.com/documentation/web-api/concepts/access-token the access token is only valid for 60 minutes.
                //As such we need to renew this token on an hourly basis.
                if (hourlyUpdate > 60 || _accessToken == null)
                {
                    return await GetToken(_clientId, _clientSecret);
                }
                return _accessToken;
            }

            public async Task<string> GetToken(string clientId, string clientSecret)
            {
                // new HTTP Request Message as cited in the documentation.
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

                // Sets the header of the request to the parameters needed to generate new token
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                {"grant_type", "client_credentials"}
                });

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Unwraps the response and returns the new token.
                var responseString = await response.Content.ReadAsStringAsync();
                var authResult = JsonSerializer.Deserialize<AuthResult>(responseString);

                return authResult.access_token;

            }
        }
    }
}