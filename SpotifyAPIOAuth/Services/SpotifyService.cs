﻿using SpotifyAPIOAuth.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SpotifyAPIOAuth.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;

        //Declaring HTTPClient within Constructor for the injection
        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream);

            return responseObject?.albums?.items.Select(i => new Release
            {
                ArtistName = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artists = string.Join(",", i.artists.Select(i => i.name))
            });
        }
    }
}
