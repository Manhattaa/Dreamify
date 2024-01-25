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
        public User AddUser(UsersDto usersDto);
        public List<UsersViewModel> GetUser(int? userId);
        public User ConnectUserToArtist(int userId, int artistId);
        public User ConnectUserToGenre(int userId, int genreId);
        public User ConnectUserToSong(int userId, int songId);
    }

    public class UsersHelper : IUsersHelper
    {
        // Create context for SongsHelper
        private ApplicationContext _context;

        public UsersHelper(ApplicationContext context)
        {
            _context = context;
        }


        public User AddUser(UsersDto usersDto)
        {           
            
            User user = new User()
            {
                 Username = usersDto.Username,
                    
            };

            _context.Users.Add(user);
            _context.SaveChanges();

             return user; 
          
        }


        public List<UsersViewModel> GetUser(int? userId)  
        {
                      
             List<UsersViewModel> users;

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


        public User ConnectUserToArtist(int userId, int artistId)
        {
          
             // Get user and artist from IDs
             User user = _context.Users
                 .Include(u => u.Artists)
                 .Where(u => u.UserId == userId)
                 .Single();  //SingleOrDefault makes a green squiggley so i changed to Single(); for now. 

             Artist artist = _context.Artists
                 .Where(a => a.ArtistId == artistId)
                 .Single();

             // Check if null
             if (user == null || artist == null)
             {
                 // Handle not found cases as needed
                 throw new Exception((user == null)
                     ? $"No user with id {userId} found"
                     : $"No artist with id {artistId} found");
             }

             // Add and save to db
             user.Artists.Add(artist);
             _context.SaveChanges();

             return user; // Return the updated User object
          
        }


        public User ConnectUserToGenre(int userId, int genreId)
        {
            
             // Get user, and genre from IDs
             User user = _context.Users
                 .Include(u => u.Genres)
                 .Where(u => u.UserId == userId).Single();

             Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

             // Check if null
             if (user == null || genre == null)

                 throw new Exception((user == null)
                     ? $"No user with id {userId} found"
                     : $"No artist with id {genreId} found");


             // Add and save to db
             user.Genres.Add(genre);
             _context.SaveChanges();

             return user;           
           
        }

        public User ConnectUserToSong(int userId, int songId)
        {
            // Get user, and song from IDs
            User user = _context.Users
                .Include(u => u.Songs)
                .Where(u => u.UserId == userId).Single();

            Song song = _context.Songs.Where(s => s.SongId == songId).Single();

            // Check if null
            if (user == null || song == null)
                throw new Exception((user == null)
                    ? $"No user with id {userId} found"
                    : $"No artist with id {songId} found");


            // Add and save to db
            user.Songs.Add(song);
            _context.SaveChanges();

            return user;
        }
    }
}
