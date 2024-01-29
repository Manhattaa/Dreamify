using Dreamify.Models.Dtos.SpotifyDtos.Artists;
using Dreamify.Models.Dtos.SpotifyDtos.Songs;
using Dreamify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dreamify.Handlers
{
    public class SpotifyHandler
    {
        public static async Task<IResult> SpotifySongSearch(string search, int? offset, string? countryCode, ISpotifyHelper spotifyService)
        {
            try
            {
                var result = await spotifyService.SpotifySongSearch(search, offset, countryCode);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        public static async Task<IResult> SpotifyArtistSearch(string search, int? offset, string? countryCode, ISpotifyHelper spotifyService)
        {
            try
            {
                var result = await spotifyService.SpotifyArtistSearch(search, offset, countryCode);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult AddSpotifySong([FromBody] AddSpotifySongDto sSongDto, ISpotifyDbHelper spotifyDbService)
        {
            try
            {
                spotifyDbService.AddSpotifySong(sSongDto.UserId, sSongDto.SongName, sSongDto.SpotifySongId, sSongDto.ArtistName, sSongDto.SpotifyArtistId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddSpotifySong(): {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult AddSpotifyArtist([FromBody] AddSpotifyArtistDto aArtistDto, ISpotifyDbHelper spotifyDbService)
        {
            try
            {
                spotifyDbService.AddSpotifyArtist(aArtistDto.UserId, aArtistDto.ArtistName, aArtistDto.SpotifyArtistId);
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific exception for user not found
                Console.WriteLine($"Error in AddSpotifyArtist - The User was not found: {ex}");
                return Results.Problem(detail: "The User was not found", statusCode: (int)HttpStatusCode.NotFound);
            }
            catch (ArgumentException ex)
            {
                // A Handler for Invalid userID
                Console.WriteLine($"Error in AddSpotifyArtist - Invalid user ID: {ex}");
                return Results.Problem(detail: "Invalid user ID", statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                // For all other exceptions
                Console.WriteLine($"Error in AddSpotifyArtist: {ex}");
                return Results.Problem(statusCode: (int)HttpStatusCode.InternalServerError);
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
