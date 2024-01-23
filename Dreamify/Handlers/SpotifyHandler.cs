using Dreamify.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dreamify.Handlers
{
    public class SpotifyHandler
    {
        public static async Task<IResult> SpotifySongSearch(string search, int? offset, string? countryCode, ISpotifyHelper spotifyService)
        {
            var result = await spotifyService.SpotifySongSearch(search, offset, countryCode);

            return Results.Json(result);
        }

        public static async Task<IResult> SpotifyArtistSearch(string search, int? offset, string? countryCode, ISpotifyHelper spotifyService)
        {
            var result = await spotifyService.SpotifyArtistSearch(search, offset, countryCode);

            return Results.Json(result);
        }

        public static async Task StartOrResumePlayback(HttpContext context)
        {
            try
            {
                // Extract the access token from the query string
                string accessToken = context.Request.Query["accessToken"];

                // Check if accessToken is provided
                if (string.IsNullOrEmpty(accessToken))
                {
                    context.Response.StatusCode = 400; // Bad Request
                    await context.Response.WriteAsync("Access token is required.");
                    return;
                }

                context.Response.StatusCode = 200; // OK
                await context.Response.WriteAsync("Playback started or resumed successfully!");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
        //public static async Task<IResult> GetCurrentPlaybackState(string accessToken)
        //{
        //    var result = await spotifyService.GetCurrentPlaybackState(accessToken);

        //    return Results.Json(result);
        //}
    }
}