using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos;
using Dreamify.Models.ViewModels;


namespace Dreamify.Handlers
{
    public class UserHandler
    {
        public static IResult GetUser(ApplicationContext context)
        {
            var user = context.Users()
            .Select(u => new UsersViewModel()
            {
                Username = u.UserName,
            })
            .ToList();               
        }

    }
}
