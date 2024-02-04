using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyTests
{
    [TestClass]
    public class ISpotifyDbHelperTests
    {
        [TestMethod]
        public async Task AddSpotifySong_AddsSongToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifySong_AddsSongToDb")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifySong(1, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");

            // Assert
            Assert.AreEqual(1, context.Songs.Count());
        }
        
        [TestMethod]
        public async Task AddSpotifySong_AddsCorrectSongToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifySong_AddsCorrectSongToDb")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifySong(1, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");

            // Assert
            Assert.AreEqual("test-songName", context.Users.Single(u => u.UserId == 1).Songs.First().Title);
        }

        [TestMethod]
        public async Task AddSpotifySong_AddsCorrectArtistToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifySong_AddsCorrectArtistToDb")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifySong(1, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");

            // Assert
            Assert.AreEqual("test-artistName", context.Users.Single(u => u.UserId == 1).Artists.First().ArtistName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AddSpotifySong_ThrowsExceptionIfUserAlreadyConnectedToSong()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifySong_AddsCorrectSongToDb")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifySong(1, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");
            spotifyDbHelper.AddSpotifySong(1, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task AddSpotifySong_ThrowsExceptionIfUserNotFound()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifySong_ThrowsExceptionIfUserNotFound")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifySong(2, "test-songName", "test-spotifySongId", "test-artistName", "test-spotifyArtistId");
        }

        [TestMethod]
        public async Task AddSpotifyArtist_CheckIfArtistMakesItToDB()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifyArtist_CheckIfArtistMakesItToDB")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "UserTest",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifyArtist(1, "spotifyArtistIdTest", "artistNameTest", "descriptionTest", 0);

            // Assert
            Assert.AreEqual(1, context.Artists.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task AddSpotifyArtist_ThrowsExceptionIfUserIsNull()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSpotifyArtist_ThrowsExceptionIfUserIsNull")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISpotifyDbHelper spotifyDbHelper = new SpotifyDbHelper(context);

            User user = new User()
            {
                UserId = 1,
                Username = "UserTest",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            spotifyDbHelper.AddSpotifyArtist(2, "spotifyArtistIdTest", "artistNameTest", "descriptionTest", 0);
        }
    }
}
