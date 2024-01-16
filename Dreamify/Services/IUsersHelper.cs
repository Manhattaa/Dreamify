using Dreamify.Data;
using Dreamify.Models.Dtos;
using Dreamify.Models.ViewModels;
using Dreamify.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Dreamify.Services
{
    public interface IUsersHelper
    {
        public IResult AddUserToDb(UsersDto usersDto);
        public IResult GetUserFromDb();
    }

    public class UsersHelper : IUsersHelper
    {
        private ApplicationContext _context;

        public UsersHelper(ApplicationContext context)
        {
            _context = context;
        }
        public IResult AddUserToDb(UsersDto usersDto)
        {
           
            User user = new User()
            {
                Username = usersDto.Username,
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);


        }

        public IResult GetUserfromDb()
        {
            var user = _context.Users()
            .Select(u => new UsersViewModel()
            {
                Username = u.UserName,
            })
            .ToList();

        }

  
    }

}
