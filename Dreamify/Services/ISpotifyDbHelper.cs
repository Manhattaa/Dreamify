using Dreamify.Data;
using Dreamify.Models;

namespace Dreamify.Services
{
    public interface ISpotifyDbHelper
    {
        void AddSpotifySong(int userId, string songName, string spotifySongId, string artistName, string spotifyArtistId);
        void AddSpotifyArtist(int userId, string artistname, string spotifyArtistId, string description, int? popularity);
    }

    public class SpotifyDbHelper : ISpotifyDbHelper
    {
        // Create context for SongsHelper
        private ApplicationContext _context;

        public SpotifyDbHelper(ApplicationContext context)
        {
            _context = context;
        }

        public void AddSpotifySong(int userId, string songName, string spotifySongId, string artistName, string spotifyArtistId)
        {
            // Get user and check if exists
            User user = _context.Users.Where(u => u.UserId == userId).Single();

            if (user == null)
                throw new NullReferenceException($"No user found with the id \"{userId}\"");


            //Check if user already is connected to this song
            if (user.Songs.Any(s => s.SpotifyId == spotifySongId))
                throw new InvalidOperationException($"User {user.Username} already has song {songName} connected to them");


            Artist artist;
            // If artist with sent in id exists in db save to artist
            if (_context.Artists.Any(a => a.SpotifyArtistId == spotifyArtistId))
            {
                artist = _context.Artists.Where(a => a.SpotifyArtistId == spotifyArtistId).Single();
            }
            // Otherwise create new artist object
            else
            {
                artist = new Artist()
                {
                    SpotifyArtistId = spotifyArtistId,
                    ArtistName = artistName,
                };
                _context.Add(artist);
                _context.SaveChanges();
            }


            Song song;
            // If song with sent in id exists in db save to song
            if (_context.Songs.Any(s => s.SpotifyId == spotifySongId))
            {
                song = _context.Songs.Where(s => s.SpotifyId == spotifySongId).Single();
            }
            // Otherwise create new song object
            else
            {
                song = new Song()
                {
                    Title = songName,
                    SpotifyId = spotifySongId,
                    Artist = artist,
                };
                _context.Add(song);
                _context.SaveChanges();
            }

            // Connect song to user
            user.Songs.Add(song);
            _context.SaveChanges();
        }

        public void AddSpotifyArtist(int userId, string name, string spotifyArtistId, string description, int? popularity)
        {
            //if (userId <= 0) //make sure userId is never 0. As it can never be 0.
            //{
            //    throw new ArgumentException($"Invalid user ID: {userId}", nameof(userId));
            //}

            //User user = _context.Users.SingleOrDefault(u => u.UserId == userId);
            User user = _context.Users.Where(u => u.UserId == userId).Single();

            if (user == null)
            {
                throw new NullReferenceException($"No user found with the id \"{userId}\"");
            }

            if (user.Artists != null && user.Artists.Any(a => a.SpotifyArtistId == spotifyArtistId))
            {
                throw new InvalidOperationException($"User {user.Username} already has {name} connected to them");
            }

            // Ensure the SpotifyArtistId is not null or empty before querying the database
            //if (string.IsNullOrEmpty(spotifyArtistId))
            //{
            //    throw new ArgumentException("Invalid SpotifyArtistId", nameof(spotifyArtistId));
            //}

            // Retrieve the artist from the database
            //Artist artist = _context.Artists.SingleOrDefault(a => a.SpotifyArtistId == spotifyArtistId);

            Artist artist;

            // If the artist is not found, create a new one
            //if (!_context.Artists.Any(a => a.SpotifyArtistId == spotifyArtistId));
            //{
                artist = new Artist()
                {
                    SpotifyArtistId = spotifyArtistId,
                    ArtistName = name,
                    Description = description,
                    Popularity = popularity
                };
                _context.Add(artist);
                _context.SaveChanges();


            user.Artists.Add(artist);
            _context.SaveChanges();
        }
    }
}
