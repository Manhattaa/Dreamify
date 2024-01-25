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
        public IResult AddSong(int artistId, int genreId, SongDto songDto);
        public IResult GetSongs();
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

        public IResult AddSong(int artistId, int genreId, SongDto songDto)
        {
            try
            {
                // Get artist and genre from their IDs
                Artist artist = _context.Artists.Where(a => a.ArtistId == artistId).Single();
                Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

                // Make sure artist and genre isn't null
                if (artist == null)
                    return Results.NotFound($"No artist found with the id {artistId}");

                if (genre == null)
                    return Results.NotFound($"No genre found with the id {genreId}");


                // Create new song object and save to db
                Song song = new Song()
                {
                    Title = songDto.Title,
                    Artist = artist,
                    Genre = genre
                };

                _context.Add(song);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }

        public IResult GetSongs()
        {
            try
            {
                // Create list of all songs 
                List<SongsViewModel> songs = _context.Songs
                    .Select(s => new SongsViewModel
                    {
                        Title = s.Title,
                    })
                    .ToList();

                return Results.Json(songs);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
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
                throw new Exception();


            // Create viewmodel consisting of username and a list of all songs
            UserSongsViewModel userSongs = new UserSongsViewModel
            {
                Username = user.Username,
                Songs = user.Songs
                .Select(u => new SongsViewModel
                {
                    Title= u.Title,
                })
                .ToList()
            };

            return userSongs;
        }

    }
    
}
