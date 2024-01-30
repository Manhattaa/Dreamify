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
            builder.Services.AddScoped<IArtistsHelper, ArtistsHelper>();
            builder.Services.AddScoped<IUsersHelper, UsersHelper>();
            builder.Services.AddScoped<ISpotifyDbHelper, SpotifyDbHelper>();
            builder.Services.AddScoped<ISpotifyHelper>(s =>
                new SpotifyHelper(
                    builder.Configuration.GetValue<string>("Spotify:ClientId"),
                    builder.Configuration.GetValue<string>("Spotify:ClientSecret"),
                    new HttpClient()
                )
            );

            var app = builder.Build();

            string version = "v1";


            app.MapGet("/", () => "Hello World!");

            // Users endpoints
            app.MapGet("/users", UserHandler.GetUser);
            app.MapGet("/users/{userId}", UserHandler.GetUser);
            app.MapGet("/users-and-id", UserHandler.GetUserAndId);
            app.MapPost("/users", UserHandler.AddUser);

            app.MapPost("/users/{userId}/artists/{artistId}", UserHandler.ConnectUserToArtist);
            app.MapPost("/users/{userId}/genres/{genreId}", UserHandler.ConnectUserToGenre);
            app.MapPost("/users/{userId}/songs/{songId}", UserHandler.ConnectUserToSong);

            // Songs endpoints
            app.MapGet("/songs", ArtistHandler.GetSongs);
            app.MapGet("/users/{userId}/songs", ArtistHandler.GetUserSongs);
            app.MapPost("/songs", ArtistHandler.AddSong);

            // Artists endpoints
            app.MapGet("/artists", ArtistHandler.GetArtist);
            app.MapPost("/artists", ArtistHandler.AddArtist);

            // Genre endpoints
            app.MapGet("/genres", ArtistHandler.GetGenres); 
            app.MapPost("/genres", ArtistHandler.AddGenres); 

            // Spotify endpoints
            app.MapGet("/search/song/{search}/{offset?}/{countryCode?}", SpotifyHandler.SpotifySongSearch);
            app.MapGet("/search/artist/{search}/{offset?}/{countryCode?}", SpotifyHandler.SpotifyArtistSearch);
            app.MapPost("/users/add-spotify-song", SpotifyHandler.AddSpotifySong);
            app.MapPost("/users/add-spotify-artist", SpotifyHandler.AddSpotifyArtist);

            app.Run();
        }
    }
}
