using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.Dtos.SpotifyDtos.Artists;
using Dreamify.Models.Dtos.SpotifyDtos.Songs;
using Dreamify.Models.ViewModels.SpotifyViewModels;

namespace Dreamify.Services
{
    public interface ISpotifyHelper
    {
        Task<List<SongSearchViewModel>> SpotifySongSearch(string search, int? offset, string? countryCode);
    }

    public class SpotifyHelper : ISpotifyHelper
    {
        private HttpClient _httpClient;
        private string _clientId;
        private string _clientSecret;
        private DateTime _hourlyToken;
        private string _accessToken;
        private int _limit = 10; // Sets default search limit on queries


        // Constructor with default HttpClient
        public SpotifyHelper(string clientId, string clientSecret) : this(clientId, clientSecret, new HttpClient()) { }


        // Constructor with HttpClient provided
        public SpotifyHelper(string clientId, string clientSecret, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
        

        private async Task<string> GetAccessToken()
        {
            var hourlyUpdate = (DateTime.Now - _hourlyToken).TotalMinutes; //https://learn.microsoft.com/en-us/dotnet/api/system.timespan.totalminutes?view=net-8.0

            // https://developer.spotify.com/documentation/web-api/concepts/access-token the access token is only valid for 60 minutes.
            //As such we need to renew this token on an hourly basis.

            // Creates a new access token if more than 60 minutes has passed or if it doesn't exist
            if (hourlyUpdate > 60 || _accessToken == null)
            {
                return await GetToken(_clientId, _clientSecret);
            }

            // Else if access token exists and the 60 min haven't passed, return the token
            return _accessToken;
        }


        private async Task<string> GetToken(string clientId, string clientSecret)
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

            if (!response.IsSuccessStatusCode)
            {
                // Log or inspect response details here
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Spotify API Error: {response.StatusCode} - {responseContent}"); // DEBUG LINE

                throw new HttpRequestException($"Spotify API Error: {response.StatusCode} - {responseContent}");
            }

           

            response.EnsureSuccessStatusCode();

            //We need to read the response. it gets returned as a Stream (https://learn.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-8.0)
            //We then deserialize it to get an object that we can actually use.
            var responseString = await response.Content.ReadAsStringAsync();
            var authResult = JsonSerializer.Deserialize<AuthResult>(responseString);

            return authResult.access_token;

        }


        // Notes for Adrian 
        // Search method (take in string query and int offset) return list of song view models

        // 1 Get token

        // 2 Call api with the search query and save in response variable
        // $"https://api.spotify.com/v1/search?q={query}&type=track&limit=10&offset={offset}"
        // query is what we're searching for, offset is how many things it should skip in the search
        // limit is how many results we should get

        // 3 Return list of songs based on search from user


        // Returns list of songs consisting of song id, song name, list of artists
        public async Task<List<SongSearchViewModel>>SpotifySongSearch(string search, int? offset, string? countryCode)
        { 
            // Get access token
            string accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            // Set default values if parameters are null
            if (string.IsNullOrEmpty(countryCode))
                countryCode = "SE";

            if (offset == null)
                offset = 10;


            // Create Spotify API query from parameters
            string query = $"https://api.spotify.com/v1/search?q={search}&type=track&market={countryCode}&limit={_limit}&offset={offset}";


            // Send GET request to the Spotify API 
            HttpResponseMessage response = await _httpClient.GetAsync(query);
            response.EnsureSuccessStatusCode();


            // Read response body and deserialize to Spotify song DTOs
            string responseBody = await response.Content.ReadAsStringAsync();
            SongSearchResponse searchResponse = JsonSerializer.Deserialize<SongSearchResponse>(responseBody);
            
            // Access list of songs from songs container
            List<SpotifySongDto> spotifySongDto = searchResponse.SongsContainer.Items;


            // Convert each song dto to a view model
            List<SongSearchViewModel> result = new List<SongSearchViewModel>();
            foreach (SpotifySongDto songDto in spotifySongDto)
            {
                // Map the SpotifySongDto to SongSearchViewModel
                SongSearchViewModel songViewModel = new SongSearchViewModel
                {
                    SpotifySongId = songDto.SpotifySongId,
                    SongName = songDto.Name,
                    Artists = songDto.Artists.Select(artistDto => new SongArtistViewModel(artistDto)).ToList(), // list of artists, maybe we only want the main artist? Discuss in team
                };

                result.Add(songViewModel);
            }

            return result;
        }

        public async Task<List<SpotifyArtistsSearchViewModel>> SpotifyArtistSearch(string search, int offset)
        {

            var accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={search}&type=artist&limit={_limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonSerializer.Deserialize<ArtistSearchResponse>(responseBody);

            var artistDtos = searchResponse.Artists.Items;

            // Converts the Dto into a ViewModel before returning.
            var artistViewModels = new List<SpotifyArtistsSearchViewModel>();
            foreach (var artistDto in artistDtos)
            {
                var artistViewModel = new SpotifyArtistsSearchViewModel
                {
                    Name = artistDto.Name,
                    SpotifyArtistId = artistDto.SpotifyArtistId,
                    Genre = artistDto.Genre

                };

                artistViewModels.Add(artistViewModel);
            }

            return artistViewModels;
        }
    }

}