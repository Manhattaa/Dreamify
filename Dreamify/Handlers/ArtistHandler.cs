using Dreamify.Data;
using Dreamify.Models.Dtos;
using Dreamify.Services;
using System.Net;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        // Post genres

        // Post artists

        // Post songs
        public static IResult AddSong(int artistId, int genreId, SongDto song, ISongsHelper songHelper)
        {
            songHelper.AddSong(artistId, genreId, song);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult GetSong(ISongsHelper songHelper)
        {
            songHelper.GetSongs();
            return Results.StatusCode((int)HttpStatusCode.OK);
        }
    }
}
