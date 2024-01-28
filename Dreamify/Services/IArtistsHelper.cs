﻿using Dreamify.Data;
using Dreamify.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public Artist AddArtist(int genreId, ArtistDto artistDto);
        public List<ArtistsViewModel> GetArtists();
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

        public Artist AddArtist(int genreId, ArtistDto artistDto)
        {
            
            
                Genre genre = _context.Genres.Where(g => g.GenreId == genreId).Single();

                Artist artist = new Artist()
                {
                    ArtistName = artistDto.ArtistName,
                    Description = artistDto.Description
                };

                _context.Artists.Add(artist);
                _context.SaveChanges();

            return artist;
                        
        }

        public List<ArtistsViewModel> GetArtists()
        {            
             List<ArtistsViewModel> artists = _context.Artists
                 .Select(a => new ArtistsViewModel
                 {
                     ArtistName = a.ArtistName,
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


            // Create viewmodel consistin of username and a list of all songs
            UserArtistsViewModel userArtists = new UserArtistsViewModel
            {
                Username = user.Username,
                Artists = user.Artists
                .Select(a => new ArtistsViewModel
                {
                    ArtistName = a.ArtistName,
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
    }
}
