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
        public IResult AddUser(UsersDto usersDto);
        public IResult GetUser(int? userId);
    }

    public class UsersHelper : IUsersHelper
    {
        public ApplicationContext _context;

        public UsersHelper(ApplicationContext context)
        {
            _context = context;
        }

        public IResult AddUser(UsersDto usersDto)
        {
            try
            {
                User user = new User()
                {
                    Username = usersDto.Username,
                    
                };

                _context.Users.Add(user);

                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                
                return Results.Text($"Error adding user to the database: {ex.Message}");
            }
        }

        public IResult GetUser(int? userId)
        {
            try
            {
                List<UsersViewModel> users;

                if (userId == null)
                {
                    users = _context.Users
                    .Select(u => new UsersViewModel
                    {
                        Username = u.Username,

                    })
                    .ToList();
                } 
                else
                {
                    users = _context.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new UsersViewModel
                    {
                        Username = u.Username,

                    }) 
                    .ToList();
                    
                }
                

                return Results.Json(users);
            }
            catch (Exception ex)
            {
                
                return Results.Text($"Error retrieving users from the database: {ex.Message}");
            }
        }

        

    }

}
