using Dreamify.Data;
using Dreamify.Models;

namespace Dreamify.Services
{
    public interface ISpotifyDbHelper
    {
        void AddSpotifySong(int userId, string songName, string spotifySongId, string artistName, string spotifyArtistId);
        void AddSpotifyArtist(int userId, string artistname, string spotifyArtistId);
    }

    public class SpotifyDbHelper : ISpotifyDbHelper
    {
        // Create context for SongsHelper
        private ApplicationContext _context;

        public SpotifyDbHelper(ApplicationContext context)
        {
            _context = context;
        }

        public void AddSpotifyArtist(int userId, string name, string spotifyArtistId)
        {
            if (userId <= 0) //make sure userId is never 0. As it can never be 0.
            {
                throw new ArgumentException($"Invalid user ID: {userId}", nameof(userId));
            }

            User user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new NullReferenceException($"No user found with the id \"{userId}\"");
            }

            if (user.Artists != null && user.Artists.Any(a => a.SpotifyArtistId == spotifyArtistId))
            {
                throw new InvalidOperationException($"User {user.Username} already has {name} connected to them");
            }

            // Ensure the SpotifyArtistId is not null or empty before querying the database
            if (string.IsNullOrEmpty(spotifyArtistId))
            {
                throw new ArgumentException("Invalid SpotifyArtistId", nameof(spotifyArtistId));
            }

            // Retrieve the artist from the database
            Artist artist = _context.Artists.SingleOrDefault(a => a.SpotifyArtistId == spotifyArtistId);

            // If the artist is not found, create a new one
            if (artist == null)
            {
                artist = new Artist()
                {
                    SpotifyArtistId = spotifyArtistId,
                    Name = name,
                    Description = "Placeholder Description", // a placeholder for the description
                };
                _context.Add(artist);
                _context.SaveChanges();
            }

            user.Artists.Add(artist);
            _context.SaveChanges();
        }
    }
}
