using System.Net;
using Dreamify.Models;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels;
using Dreamify.Data;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Services
{
    public interface IGenresHelper
    {
        public IResult AddGenre(GenreDto genreDto);

        public IResult GetGenre();
    }

        public class GenreHelper : IGenresHelper
        {
        private ApplicationContext _context;
        public GenreHelper(ApplicationContext context)
        {
            _context = context;
        }
        public IResult AddGenre(GenreDto genreDto)
        {
            try
            {
                Genre genre = new Genre
                {
                    Title = genreDto.Title
                };
                _context.Genres.Add(genre);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }
        public IResult GetGenre()
        {
            try
            {
                List<GenreViewModel> genres = _context.Genres
              .Select(g => new GenreViewModel
              {
                  Title = g.Title,
              })
              .ToList();

                return Results.Json(genres);
            }
            catch(Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }
    }

    
}
