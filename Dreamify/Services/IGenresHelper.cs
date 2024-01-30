using System.Net;
using Dreamify.Models;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels;
using Dreamify.Data;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Models.ViewModels.DreamifyViewModels;

namespace Dreamify.Services
{
    public interface IGenresHelper
    {
        public void AddGenre(GenreDto genreDto);
        public List<GenresViewModel> GetGenres();
        public UserGenresViewModel GetUserGenres(int userId);
    }

    public class GenresHelper : IGenresHelper
    {
        private ApplicationContext _context;
        public GenresHelper(ApplicationContext context)
        {
            _context = context;
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
    }   
}
