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

        public static async Task StartOrResumePlayback(HttpContext context, ISpotifyHelper spotifyService)
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

                // Call the StartOrResumePlayback method from your Spotify service
                await spotifyService.StartOrResumePlayback(accessToken);

                context.Response.StatusCode = 200; // OK
                await context.Response.WriteAsync("Playback started or resumed successfully!");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
        public static async Task<IResult> GetCurrentPlaybackState(HttpContext context, string accessToken, ISpotifyHelper spotifyService)
        {
            //try
            //{
                //// Check if accessToken is provided
                //if (string.IsNullOrEmpty(accessToken))
                //{
                //    context.Response.StatusCode = 400; // Bad Request
                //    await context.Response.WriteAsync("Access token is required.");
                //    return Results.Json("Access token is required.");
                //}

                var result = await spotifyService.GetCurrentPlaybackState(accessToken);

            Console.WriteLine(result);
            return Results.Json(result);
            //}
            //catch (Exception ex)
            //{
            //    context.Response.StatusCode = 500; // Internal Server Error
            //    await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            //    return Results.Json($"An error occurred: {ex.Message}");
            //}
        }
    }
}