using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels;
using Dreamify.Services;

namespace Dreamify.Handlers
{
    public class UserHandler
    {

        public static IResult GetUser(int? userId, IUsersHelper usersHelper)
        {
            var results = usersHelper.GetUser(userId);
            return Results.Json(results);
        }

        public static IResult AddUser(UsersDto usersDto, IUsersHelper usersHelper)
        {
            return (IResult)usersHelper.AddUser(usersDto);
        }

        public static IResult ConnectUserToArtist(int userId, int artistId, IUsersHelper userHelper)
        {
            return (IResult)userHelper.ConnectUserToArtist(userId, artistId);
        }

        public static IResult ConnectUserToGenre(int userId, int genreId, IUsersHelper userHelper)
        {
            return (IResult)userHelper.ConnectUserToGenre(userId, genreId);
        }

        public static IResult ConnectUserToSong(int userId, int songId, IUsersHelper userHelper)
        {
            return  (IResult)userHelper.ConnectUserToSong(userId, songId);
        }

    }
}
