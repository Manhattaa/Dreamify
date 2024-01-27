using Dreamify.Services;
using System.Net;
using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Microsoft.IdentityModel.Logging;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        public static IResult AddArtist(ApplicationContext context, string personId, ArtistDto artistDto)
        {
            try
            {
                // move this code to the IArtistHelper
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
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult GetArtist(IArtistsHelper artistHelper)
        {
            try
            {
                return Results.Json(artistHelper.GetArtists());
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }


        public static IResult AddSong(int artistId, int genreId, SongDto song, ISongsHelper songHelper)
        {
            try
            {
                songHelper.AddSong(artistId, genreId, song);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }

        }

        public static IResult GetSongs(ISongsHelper songHelper)
        {
            try
            { 
                return Results.Json(songHelper.GetSongs());
            }
            catch(Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }


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
