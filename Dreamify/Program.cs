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
            string clientId = builder.Configuration.GetValue<string>("Spotify:ClientId");
            string clientSecret = builder.Configuration.GetValue<string>("Spotify:ClientSecret");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ISongsHelper, SongsHelper>();
            builder.Services.AddScoped<IUsersHelper, UsersHelper>();
            builder.Services.AddScoped<ISpotifyHelper>(s =>
                new SpotifyHelper(
                    builder.Configuration.GetValue<string>("Spotify:ClientId"),
                    builder.Configuration.GetValue<string>("Spotify:ClientSecret"),
                    new HttpClient()
                )
            );

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
            //app.MapGet("/artists/{artistId}", ArtistHandler.GetArtist);


            // Spotify endpoints
            app.MapGet("/search/song/{search}/{offset?}/{countryCode?}", SpotifyHandler.SpotifySongSearch);

            app.Run();

            app.MapPost("/genres", ArtistHandler.GetArtist);
            app.MapGet("/genres/{genreId}", ArtistHandler.GetGenre);
            app.Run();
        }
    }
}
