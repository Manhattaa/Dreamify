using Dreamify.Data;
using Dreamify.Handlers;
using Dreamify.Models;
using Dreamify.Models.Dtos;
using Dreamify.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dreamify;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace DreamifyTests
{
    [TestClass]
    public class ISongsHelperTests
    {
        [TestMethod]
        public void GetSongs_ReturnsListOfSongs()
        {
            var mockService = new Mock<ISongsHelper>();
            ISongsHelper songsHelper = mockService.Object;

            songsHelper.GetSongs();
            
            
            // ???
        }

        [TestMethod]
        public void AddSong_AddsNewSongToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            SongsHelper songsHelper = new SongsHelper(context);

            // Act
            //songsHelper.AddSong(1, 2, new SongDto
            //{
            //    Title = "TestTitle",
            //});


            // Assert
            Assert.AreEqual(1, context.Songs.Count());

        }

        [TestMethod]
        public void AddSong_AddsCorrectInputToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            SongsHelper songsHelper = new SongsHelper(context);

            // Act
            //songsHelper.AddSong(1, 2, new SongDto
            //{
            //    Title = "TestTitle",
            //});

            Artist testArtist = new() { ArtistId = 1};
            Genre testGenre = new() { GenreId = 2 };

            Song verification = new()
            {
                SongId = 1,
                Title = "TestTile",
                Artist = testArtist,
                Genre =  testGenre
            };


            // Assert
            Assert.AreEqual(verification, context.Songs.Select(s => new 
            {
                s.SongId,
                s.Title,
                s.Artist.ArtistId,
                s.Genre.GenreId
            })
            .Single());

        }


        //public void GetUserSongs(int userId);






    }
}
