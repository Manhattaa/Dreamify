using Dreamify.Data;
using Dreamify.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public IResult AddArtist(int genreId, ArtistDto artistDto);
        public IResult GetArtists();
        public IResult GetUserArtists(int userId);
        public IResult GetUserGenres(int userId);


    }

    public class ArtistsHelper : IArtistsHelper
    {
        private ApplicationContext _context { get; set; }
        public ArtistsHelper(ApplicationContext context)
        {
            _context = context;
        }

        public IResult AddArtist(int genreId, ArtistDto artistDto)
        {
            try
            {
                Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

                Artist artist = new Artist()
                {
                    Name = artistDto.Name,
                    Description = artistDto.Description
                };

                _context.Artists.Add(artist);
                _context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }

        public IResult GetArtists()
        {
            try
            {
                List<ArtistsViewModel> artists = _context.Artists
                    .Select(a => new ArtistsViewModel
                    {
                        Name = a.Name,
                    })
                    .ToList();
                return Results.Json(artists);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }

        public IResult GetUserArtists(int userId)
        {
            try
            {
                // Get user and their artists from id
                User user = _context.Users
                .Include(u => u.Artists)
                .Where(u => u.UserId == userId)
                .Single();

                if (user == null)
                    return Results.NotFound($"No user found with id {userId}");


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


                return Results.Json(userArtists);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }

        public IResult GetUserGenres(int userId)
        {
            try
            {
                // Get user and their artists from id
                User user = _context.Users
                .Include(u => u.Genres)
                .Where(u => u.UserId == userId)
                .Single();

                if (user == null)
                    return Results.NotFound($"No user found with id {userId}");


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


                return Results.Json(userGenres);
            }
            catch (Exception ex)
            {
                return Results.Text(ex.Message);
            }
        }
    }
}
