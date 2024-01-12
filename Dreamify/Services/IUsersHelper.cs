using Dreamify.Data;
using Dreamify.Models;
using System.Net;

namespace Dreamify.Services
{
    public interface IUsersHelper
    {
        public IResult ConnectUserToArtist(int userId, int artistId);
        public IResult ConnectUserToGenre(int userId, int genreId);
        public IResult ConnectUserToSong(int userId, int songId);

    }

    public class UsersHelper : IUsersHelper
    {
        // Create context for SongsHelper
        private ApplicationContext _context;

        public UsersHelper(ApplicationContext context)
        {
            _context = context;
        }

        public IResult ConnectUserToArtist(int userId, int artistId)
        {
            try
            {
                // Get user, and artist from IDs
                User user = _context.Users.Where(u => u.UserId == userId).Single();
                Artist artist = _context.Artists.Where(a => a.ArtistId == artistId).Single();

                // Check if null
                if (user == null || artist == null)
                    return Results.NotFound((user == null)
                        ? $"No user with id {userId} found"
                        : $"No artist with id {artistId} found");


                // Add and save to db
                user.Artists.Add(artist);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex) 
            {
                return Results.Text(ex.Message);
            }


        }

        public IResult ConnectUserToGenre(int userId, int genreId)
        {
            try
            {
                // Get user, and genre from IDs
                User user = _context.Users.Where(u => u.UserId == userId).Single();
                Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

                // Check if null
                if (user == null || genre == null)
                    return Results.NotFound((user == null)
                        ? $"No user with id {userId} found"
                        : $"No artist with id {genreId} found");


                // Add and save to db
                user.Genres.Add(genre);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }

        public IResult ConnectUserToSong(int userId, int songId)
        {
            try
            {
                // Get user, and song from IDs
                User user = _context.Users.Where(u => u.UserId == userId).Single();
                Song song = _context.Songs.Where(s => s.SongId == songId).Single();

                // Check if null
                if (user == null || song == null)
                    return Results.NotFound((user == null)
                        ? $"No user with id {userId} found"
                        : $"No artist with id {songId} found");


                // Add and save to db
                user.Songs.Add(song);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }
    }
}
