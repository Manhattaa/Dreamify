using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Services;
using Dreamify.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Dreamify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IUsersHelper, UsersHelper>();
            var app = builder.Build();


            app.MapGet("/", () => "Hello World!");


            app.MapGet("/users", UserHandler.GetUser);
            app.MapGet("/user/{userId}", UserHandler.GetUser);
            app.MapPost("/user", UserHandler.AddUser);



            app.Run();
        }
    }
}
