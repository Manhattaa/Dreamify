using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dreamify.Services
{

    public interface ISongsHelper
    {
        public Song AddSong(int artistId, int genreId, SongDto songDto);
        public List<SongsViewModel> GetSongs();
        public UserSongsViewModel GetUserSongs(int userId);
    }

    public class SongsHelper : ISongsHelper
    {
        // Create context
        private ApplicationContext _context;

        public SongsHelper(ApplicationContext context)
        {
            _context = context;
        }

        public Song AddSong(int artistId, int genreId, SongDto songDto)
        {
                      
             // Get artist and genre from their IDs
             Artist artist = _context.Artists.Where(a => a.ArtistId == artistId).Single();
             Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

             // Make sure artist and genre isn't null
             if (artist == null)
                 throw new Exception($"No artist found with the id {artistId}");

             if (genre == null)
                 throw new Exception($"No genre found with the id {genreId}");


             // Create new song object and save to db
             Song song = new Song()
             {
                 Title = songDto.Title,
                 Artist = artist,
                 Genre = genre
             };

             _context.Add(song);
             _context.SaveChanges();

             return song;
                      
        }

        public List<SongsViewModel> GetSongs()
        {
           
             // Create list of all songs 
             List<SongsViewModel> songs = _context.Songs
                 .Select(s => new SongsViewModel
                 {
                     Title = s.Title,
                 })
                 .ToList();

             return songs;
                       
        
        }

        // This is correctly formatted and structured
        public UserSongsViewModel GetUserSongs(int userId)
        {

            // Get user and their songs from id
            User user = _context.Users
            .Include(u => u.Songs)
            .Where(u => u.UserId == userId)
            .Single();

            if (user == null)
            {
                throw new Exception($"User was not found");
            }
                      

            // Create viewmodel consisting of username and a list of all songs
            UserSongsViewModel userSongs = new UserSongsViewModel
            {
                Username = user.Username,
                Songs = user.Songs
                .Select(u => new SongsViewModel
                {
                    Title = u.Title,
                })
                .ToList()
            };

            return userSongs;
        }

    }
    
}
