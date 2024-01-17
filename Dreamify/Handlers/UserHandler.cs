    using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos;
using Dreamify.Models.ViewModels;
using Dreamify.Services;

namespace Dreamify.Handlers
{
    public class UserHandler
    {

        public static IResult GetUser(int? userId, IUsersHelper usersHelper)
        {
            return usersHelper.GetUser(userId);
        }

        public static IResult AddUser(UsersDto usersDto, IUsersHelper usersHelper)
        {
            return usersHelper.AddUser(usersDto);
        }

        public static IResult ConnectUserToArtist(int userId, int artistId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToArtist(userId, artistId);
        }

        public static IResult ConnectUserToGenre(int userId, int genreId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToGenre(userId, genreId);
        }

        public static IResult ConnectUserToSong(int userId, int songId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToSong(userId, songId);
        }

    }
}
