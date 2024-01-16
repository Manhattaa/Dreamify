using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos;
using Dreamify.Models.ViewModels;
using Dreamify.Services;

namespace Dreamify.Handlers
{
    public class UserHandler
    {
        // Post usernames
        public static IResult ConnectUserToArtist(int userId, int artistId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToArtist(userId, artistId);  // Temp change this later
        }

        public static IResult ConnectUserToGenre(int userId, int genreId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToGenre(userId, genreId);  // Temp change this later
        }

        public static IResult ConnectUserToSong(int userId, int songId, IUsersHelper userHelper)
        {
            return userHelper.ConnectUserToSong(userId, songId);  // Temp change this later
        }
    }
}
