using Dreamify.Data;
using Dreamify.Models.Dtos;
using Dreamify.Services;
using System.Net;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        // Genres

        // Artists

        // Songs
        public static IResult AddSong(int artistId, int genreId, SongDto song, ISongsHelper songHelper)
        {
            songHelper.AddSong(artistId, genreId, song);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult GetSongs(ISongsHelper songHelper)
        {
            return Results.Json(songHelper.GetSongs());
        }

        public static IResult GetUserSongs(int userId, ISongsHelper songHelper)
        {
            return Results.Json(songHelper.GetUserSongs(userId));
        }
    }
}
