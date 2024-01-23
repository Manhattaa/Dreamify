using Dreamify.Services;

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

        public static async Task<IResult> StartOrResumePlayback(string accessToken, ISpotifyHelper spotifyService)
        {
            await spotifyService.StartOrResumePlayback(accessToken);

            // Since StartOrResumePlayback is asynchronous void, we return a successful result here
            return Results.Ok();
        }
    }
}
