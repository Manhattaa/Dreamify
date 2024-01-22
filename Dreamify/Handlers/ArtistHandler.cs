using Dreamify.Services;
using System.Net;
using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        // Genres
        public static IResult AddGenre(GenreDto genreDto, IGenresHelper genreHelper)
        {
            genreHelper.AddGenre(genreDto);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
        public static IResult GetGenre(IGenresHelper genreHelper)
        {
            return Results.Json(genreHelper.GetGenre());
        }
        // Artists

        public static IResult AddArtist(ApplicationContext context, string personId, ArtistDto artistDto)
        {
            try
            {
                Artist artist = new Artist
                {
                    Name = artistDto.Name,
                    Description = artistDto.Description
                };
                context.Artists.Add(artist);
                context.SaveChanges();
                return Results.Ok($"Artist {artist.Name} has been added to the database.");
            }
            catch (Exception ex)
            {
                return Results.Text($"404: Not found! {ex}");
            }
        }
        public static IResult GetArtist(IArtistsHelper artistHelper)
        {
            artistHelper.GetArtists();
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        // Post songs
        public static IResult AddSong(int artistId, int genreId, SongDto song, ISongsHelper songHelper)
        {
            songHelper.AddSong(artistId, genreId, song);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult GetSongs(ISongsHelper songHelper)
        {
            return Results.Json(songHelper.GetSongs());
        }

        // This is correctly formatted and structured
        public static IResult GetUserSongs(int userId, ISongsHelper songHelper)
        {
            try
            {
                return Results.Json(songHelper.GetUserSongs(userId));
            }
            catch (Exception ex) 
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
