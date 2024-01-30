using Dreamify.Services;
using System.Net;
using Dreamify.Models.Dtos.DreamifyDtos;
using Microsoft.AspNetCore.Mvc;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        public static IResult AddGenres([FromBody] GenreDto genreDto, IGenresHelper genreHelper)
        {
            try
            {
                genreHelper.AddGenre(genreDto);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult GetGenres(IGenresHelper genreHelper)
        {
            try
            {
                return Results.Json(genreHelper.GetGenres());
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult AddArtist([FromBody]ArtistDto artistDto, IArtistsHelper artistHelper)
        {
            try
            {
                artistHelper.AddArtist(artistDto);
                return Results.StatusCode((int)HttpStatusCode.Created);
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

        public static IResult AddSong([FromBody] SongDto songDto, ISongsHelper songHelper)
        {
            try
            {
                songHelper.AddSong(songDto);
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
