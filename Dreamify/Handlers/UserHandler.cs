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

    }
}
