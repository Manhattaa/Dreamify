using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dreamify.Services
{
    public interface IUsersHelper
    {
        
        public void AddUser(UsersDto usersDto);

        
        public List<UsersViewModel> GetUser(int? userId);

       
        public List<UsersIdViewModel> GetUserAndId();

        
        public void ConnectUserToArtist(int userId, int artistId);
        public void ConnectUserToGenre(int userId, int genreId);
        public void ConnectUserToSong(int userId, int songId);
    }

    public class UsersHelper : IUsersHelper
    {
        
        private ApplicationContext _context;

        
        public UsersHelper(ApplicationContext context)
        {
            _context = context;
        }

        // Method to add a new user to the database
        public void AddUser(UsersDto usersDto)
        {
            // Create a new User object with the provided username
            User user = new User()
            {
                Username = usersDto.Username,
            };
           
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Method to get a list of users based on specified userId input
        public List<UsersViewModel> GetUser(int? userId)
        {
            List<UsersViewModel> users;

            // If userId is null, retrieve all users. otherwise, retrieve the user with the specified userId
            if (userId == null)
            {
                users = _context.Users
                    .Select(u => new UsersViewModel
                    {
                        Username = u.Username,
                    })
                    .ToList();
            }
            else
            {
                users = _context.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new UsersViewModel
                    {
                        Username = u.Username,
                    })
                    .ToList();
            }

            return users;
        }

        // Method to get a list of users along with their IDs
        public List<UsersIdViewModel> GetUserAndId()
        {
            List<UsersIdViewModel> users;

            // Retrieve all users and their IDs
            users = _context.Users
                .Select(u => new UsersIdViewModel
                {
                    Id = u.UserId,
                    Username = u.Username,
                })
                .ToList();

            return users;
        }

        // Method to connect a user to an artist in the database
        public void ConnectUserToArtist(int userId, int artistId)
        {
            // Get user and artist from IDs
            User user = _context.Users
                .Include(u => u.Artists)
                .Where(u => u.UserId == userId)
                .Single();

            Artist artist = _context.Artists
                .Where(a => a.ArtistId == artistId)
                .Single();

            // Check if user or artist is null, and throw an exception if either is not found
            if (user == null || artist == null)
            {
                throw new Exception((user == null)
                    ? $"No user with id {userId} found"
                    : $"No artist with id {artistId} found");
            }
            
            user.Artists.Add(artist);
            _context.SaveChanges();
        }

        // Method to connect a user to a genre in the database
        public void ConnectUserToGenre(int userId, int genreId)
        {
            // Get user and genre from IDs
            User user = _context.Users
                .Include(u => u.Genres)
                .Where(u => u.UserId == userId)
                .Single();

            Genre genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Single();

            // Check if user or genre is null, and throw an exception if either is not found
            if (user == null || genre == null)
            {
                throw new Exception((user == null)
                    ? $"No user with id {userId} found"
                    : $"No genre with id {genreId} found");
            }
           
            user.Genres.Add(genre);
            _context.SaveChanges();
        }

        // Method to connect a user to a song in the database
        public void ConnectUserToSong(int userId, int songId)
        {
            // Get user and song from IDs
            User user = _context.Users
                .Include(u => u.Songs)
                .Where(u => u.UserId == userId)
                .Single();

            Song song = _context.Songs
                .Where(s => s.SongId == songId)
                .Single();

            // Check if user or song is null, and throw an exception if either is not found
            if (user == null || song == null)
            {
                throw new Exception((user == null)
                    ? $"No user with id {userId} found"
                    : $"No song with id {songId} found");
            }
           
            user.Songs.Add(song);
            _context.SaveChanges();
        }
    }
}
