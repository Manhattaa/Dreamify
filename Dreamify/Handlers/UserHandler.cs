using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Dreamify.Services;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Dreamify.Handlers
{
    public class UserHandler
    {
        public static IResult GetUser(int? userId, IUsersHelper usersHelper)
        {
            List<UsersViewModel> results;
            try
            {
                results = usersHelper.GetUser(userId);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
            return Results.Json(results);
        }

        public static IResult AddUser(UsersDto usersDto, IUsersHelper usersHelper)
        {
            try
            {
                usersHelper.AddUser(usersDto);
            }
            catch (Exception ex) 
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        public static IResult ConnectUserToArtist(int userId, int artistId, IUsersHelper userHelper)
        {
            try
            {
                userHelper.ConnectUserToArtist(userId, artistId);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.OK);
            
        }

        public static IResult ConnectUserToGenre(int userId, int genreId, IUsersHelper userHelper)
        {
            try
            {
                userHelper.ConnectUserToGenre(userId, genreId);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        public static IResult ConnectUserToSong(int userId, int songId, IUsersHelper userHelper)
        {
            try
            {
                userHelper.ConnectUserToSong(userId, songId);
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Got exception", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

    }
}
