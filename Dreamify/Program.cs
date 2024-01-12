using Dreamify.Data;
using Dreamify.Handlers;
using Dreamify.Services;
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

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/songs", ArtistHandler.GetSongs);
            app.MapGet("/users/{userId}/songs", ArtistHandler.GetUserSongs);
            app.MapPost("/artists/{artistId}/genre/{genreId}/songs", ArtistHandler.AddSong);
            app.MapPost("/users/{userId}/artists/{artistId}", UserHandler.ConnectUserToArtist);
            app.MapPost("/users/{userId}/genres/{genreId}", UserHandler.ConnectUserToGenre);
            app.MapPost("/users/{userId}/songs/{songId}", UserHandler.ConnectUserToSong);

            app.Run();
        }
    }
}
