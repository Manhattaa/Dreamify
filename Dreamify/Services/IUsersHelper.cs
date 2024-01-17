using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos;
using Dreamify.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dreamify.Services
{
    public interface IUsersHelper
    {
        public IResult AddUser(UsersDto usersDto);
        public IResult GetUser(int? userId);
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


        public IResult AddUser(UsersDto usersDto)
        {
            try
            {
                User user = new User()
                {
                    Username = usersDto.Username,
                    
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                
                return Results.Text($"Error adding user to the database: {ex.Message}");
            }
        }


        public IResult GetUser(int? userId)
        {
            try
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
                

                return Results.Json(users);
            }
            catch (Exception ex)
            {
                
                return Results.Text($"Error retrieving users from the database: {ex.Message}");
            }
        }        


        public IResult ConnectUserToArtist(int userId, int artistId)
        {
            try
            {
                // Get user, and artist from IDs
                User user = _context.Users
                    .Include(u => u.Artists)
                    .Where(u => u.UserId == userId).Single();
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
                User user = _context.Users
                    .Include(u => u.Genres)
                    .Where(u => u.UserId == userId).Single();

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
                User user = _context.Users
                    .Include(u => u.Songs)
                    .Where(u => u.UserId == userId).Single();

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
