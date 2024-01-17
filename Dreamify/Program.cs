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
            builder.Services.AddScoped<ISongsHelper, SongsHelper>();
            builder.Services.AddScoped<IUsersHelper, UsersHelper>();
            var app = builder.Build();


            app.MapGet("/", () => "Hello World!");

            app.MapGet("/users", UserHandler.GetUser);
            app.MapGet("/user/{userId}", UserHandler.GetUser);
            app.MapPost("/user", UserHandler.AddUser);
            app.MapGet("/songs", ArtistHandler.GetSongs);
            app.MapGet("/users/{userId}/songs", ArtistHandler.GetUserSongs);
            app.MapPost("/artists/{artistId}/genre/{genreId}/songs", ArtistHandler.AddSong);
            app.MapPost("/users/{userId}/artists/{artistId}", UserHandler.ConnectUserToArtist);
            app.MapPost("/users/{userId}/genres/{genreId}", UserHandler.ConnectUserToGenre);
            app.MapPost("/users/{userId}/songs/{songId}", UserHandler.ConnectUserToSong);

            app.MapPost("/artists", ArtistHandler.AddArtist);
            app.MapGet("/artists/{artistId}", ArtistHandler.GetArtist);
          
            app.Run();
        }
    }
}
