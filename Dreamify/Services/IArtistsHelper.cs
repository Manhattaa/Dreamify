using Dreamify.Data;
using Dreamify.Models.Dtos;
using Dreamify.Models;
using Dreamify.Models.ViewModels;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public IResult AddArtist(int genreId, ArtistDto artistDto, IArtistsHelper artistHelper);


        public IResult GetArtists();
    }

    public class ArtistsHelper : IArtistsHelper
    {
        private ApplicationContext artistContext { get; set; }
        public ArtistsHelper(ApplicationContext context)
        {
            artistContext = context;
        }

        public IResult AddArtist(int genreId, ArtistDto artistDto, IArtistsHelper artistHelper)
        {
            try
            {
                Genre genre = artistContext.Genres.Where(g => g.GenreId == genreId).Single();

                Artist artist = new Artist()
                {
                    Name = artistDto.Name,
                    Description = artistDto.Description
                };

                artistContext.Artists.Add(artist);
                artistContext.SaveChanges();

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
                List<ArtistsViewModel> artists = artistContext.Artists
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
        }
    }
