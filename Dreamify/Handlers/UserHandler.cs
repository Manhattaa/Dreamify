using Dreamify.Models.Dtos;
using Dreamify.Services;

namespace Dreamify.Handlers
{
    public class UserHandler
    {
        // Post usernames
        public static IResult ConnectUserToArtist(int userId, int artistId, IUsersHelper userHelper)
        {
            userHelper.ConnectUserToArtist(userId, artistId);
            return Results.Ok();  // Temp change this later
        }

        public static IResult ConnectUserToGenre(int userId, int genreId, IUsersHelper userHelper)
        {
            userHelper.ConnectUserToGenre(userId, genreId);
            return Results.Ok();  // Temp change this later
        }

        public static IResult ConnectUserToSong(int userId, int songId, IUsersHelper userHelper)
        {
            userHelper.ConnectUserToSong(userId, songId);
            return Results.Ok();  // Temp change this later
        }
    }
}
