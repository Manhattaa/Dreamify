using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Services;
using Microsoft.EntityFrameworkCore;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace DreamifyTests
{
    [TestClass]
    public class ISongsHelperTests
    {
        // GetSongs tests
        [TestMethod]
        public void GetSongs_FindsAllSongsInDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSongs_ReturnsListOfSongsViewModel")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            Song testSong1 = new Song()
            {
                Title = "Test-title1",
            };
            Song testSong2 = new Song()
            {
                Title = "Test-title2",
            };
            Song testSong3 = new Song()
            {
                Title = "Test-title3",
            };


            context.Songs.Add(testSong1);
            context.Songs.Add(testSong2);
            context.Songs.Add(testSong3);
            context.SaveChanges();

            // Act
            var result = songsHelper.GetSongs();

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetSongs_GetsCorrectTitle()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSongs_GetsCorrectTitle")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            Song testSong = new Song()
            {
                Title = "Test-title",
            };

            context.Songs.Add(testSong);
            context.SaveChanges();


            // Act
            var result = songsHelper.GetSongs();

            // Assert
            Assert.AreEqual("Test-title", result.First().Title);
        }

        [TestMethod]
        public void GetSongs_ReturnsEmptyListWhenDbIsEmpty()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSongs_ReturnsEmptyListWhenDbIsEmpty")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            // Act
            var result = songsHelper.GetSongs();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        // AddSongs tests
        [TestMethod]
        public void AddSong_AddsNewSongToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSong_AddsNewSongToDb")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);

            // Act
            songsHelper.AddSong( new SongDto
            {
                Title = "Test-title",
            });


            // Assert
            Assert.AreEqual(1, context.Songs.Count());
        }

        [TestMethod]
        public void AddSong_AddsCorrectInputToDb()
        {
            //    // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSong_AddsCorrectInputToDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);

            // Act
            songsHelper.AddSong(new SongDto
            {
                Title = "Test-title",
            });

            // Assert
            Assert.AreEqual("Test-title", context.Songs.First().Title);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddSong_ThrowsExceptionWhenTitleIsNull()
        {
            //    // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddSong_ThrowsExceptionWhenTitleIsNull")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);

            // Act
            songsHelper.AddSong(new SongDto
            {
                Title = null,
            });
        }

        // GetUserSongs tests
        [TestMethod]
        public void GetUserSongs_FindsCorrectUserSong()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSongs_GetsCorrectTitle")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            User testUser = new User()
            {
                UserId = 1,
                Username = "Test-username",
            };

            Song testSong = new Song()
            {
                Title = "Test-title"
            };

            context.Users.Add(testUser);
            testUser.Songs.Add(testSong);
            context.SaveChanges();


            // Act
            var result = songsHelper.GetUserSongs(1);

            // Assert
            Assert.AreEqual("Test-title", result.Songs.First().Title);
        }

        [TestMethod]
        public void GetUserSongs_FindsCorrectAmountOfSongs()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUserSongs_FindsCorrectAmountOfSongs")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            Song testSong1 = new Song()
            {
                Title = "Test-title1"
            };

            Song testSong2 = new Song()
            {
                Title = "Test-title2"
            };

            Song testSong3 = new Song()
            {
                Title = "Test-title3"
            };

            User testUser = new User()
            {
                UserId = 1,
                Username = "Test-username",
                Songs = new List<Song>()
                {
                    testSong1, testSong2, testSong3
                }
            };

            context.Users.Add(testUser);
            context.SaveChanges();


            // Act
            var result = songsHelper.GetUserSongs(1);

            // Assert
            Assert.AreEqual(3, result.Songs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUserSongs_ThrowsExceptionWHenUserIsNotFound()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUserSongs_FindsCorrectAmountOfSongs")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            ISongsHelper songsHelper = new SongsHelper(context);


            Song testSong = new Song()
            {
                Title = "Test-title"
            };

            User testUser = new User()
            {
                UserId = 1,
                Username = "Test-username",
                Songs = new List<Song>()
                {
                    testSong
                }
            };

            context.Users.Add(testUser);
            context.SaveChanges();


            // Act
            var result = songsHelper.GetUserSongs(2);
        }
    }
}
