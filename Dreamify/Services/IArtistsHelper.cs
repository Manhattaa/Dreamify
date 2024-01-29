using Dreamify.Data;
using Dreamify.Models;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public void AddArtist(ArtistDto artistDto);
        public void AddGenre(GenreDto genreDto);
        public List<ArtistsViewModel> GetArtists();
        public List<GenresViewModel> GetGenres();
        public UserArtistsViewModel GetUserArtists(int userId);
        public UserGenresViewModel GetUserGenres(int userId);
    }

    public class ArtistsHelper : IArtistsHelper
    {
        private ApplicationContext _context { get; set; }
        public ArtistsHelper(ApplicationContext context)
        {
            _context = context;
        }

        public void AddArtist(ArtistDto artistDto)
        {
            Artist artist = new Artist()
            {
                Name = artistDto.Name,
                Description = artistDto.Description,
                Genres = artistDto.Genres
            };

            _context.Artists.Add(artist);
            _context.SaveChanges();          
        }

        public List<ArtistsViewModel> GetArtists()
        {            
             List<ArtistsViewModel> artists = _context.Artists
                 .Select(a => new ArtistsViewModel
                 {
                     Name = a.Name,
                 })
                 .ToList();
             return artists;          
        }

        public UserArtistsViewModel GetUserArtists(int userId)
        {

            // Get user and their artists from id
            User user = _context.Users
            .Include(u => u.Artists)
            .Where(u => u.UserId == userId)
            .Single();

            if (user == null)
                throw new Exception();


            // Create viewmodel consisting of username and a list of all songs
            UserArtistsViewModel userArtists = new UserArtistsViewModel
            {
                Username = user.Username,
                Artists = user.Artists
                .Select(a => new ArtistsViewModel
                {
                    Name = a.Name,
                    Description = a.Description
                })
                .ToList()
            };

            return userArtists;
        }

        public UserGenresViewModel GetUserGenres(int userId)
        {
            // Get user and their artists from id
            User user = _context.Users
            .Include(u => u.Genres)
            .Where(u => u.UserId == userId)
            .Single();

            if (user == null)
                throw new Exception();


            // Create viewmodel consisting of username and a list of all songs
            UserGenresViewModel userGenres = new UserGenresViewModel
            {
                Username = user.Username,
                Genres = user.Genres
                .Select(a => new GenresViewModel
                {
                    Title = a.Title,
                })
                .ToList()  
            };

            return userGenres;
        }

        public List<GenresViewModel> GetGenres()
        {
            List<GenresViewModel> genres = _context.Genres
                .Select(g => new GenresViewModel
                {
                    Title = g.Title,
                })
                .ToList();

            return genres;
        }

        public void AddGenre(GenreDto genreDto)
        {
            Genre genre = new Genre()
            {
                Title = genreDto.Title
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

    }
}
