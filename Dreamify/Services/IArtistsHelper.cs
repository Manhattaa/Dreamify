﻿using Dreamify.Data;
using Dreamify.Models;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.ViewModels.DreamifyViewModels;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public void AddArtist(ArtistDto artistDto);
        public List<ArtistsViewModel> GetArtists();
        public UserArtistsViewModel GetUserArtists(int userId);

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
                ArtistName = artistDto.ArtistName,
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
                     ArtistName = a.ArtistName,
                     Description = a.Description,
                     Popularity = a.Popularity
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
                    ArtistName = a.ArtistName,
                    Description = a.Description
                })
                .ToList()
            };

            return userArtists;
        }
    }
}
