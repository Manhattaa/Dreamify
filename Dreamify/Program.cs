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

            app.MapPost("/artists", ArtistHandler.AddArtist);
            app.MapGet("/artists/{artistId}", ArtistHandler.GetArtist);
            app.Run();

            app.MapPost("/genres", ArtistHandler.GetArtist);
            app.MapGet("/genres/{genreId}", ArtistHandler.GetGenre);
            app.Run();
        }
    }
}
