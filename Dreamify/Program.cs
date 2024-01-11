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

            app.MapPost("/artists/{artistId}/genre/{genreId}/songs", ArtistHandler.AddSong);

            app.Run();
        }
    }
}
