using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Dreamify.Data;
using Dreamify.Handlers;
using Dreamify.Models.Dtos;
using Dreamify.Models;
using Dreamify.Models.ViewModels;
using Dreamify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace DreamifyTests
{
    [TestClass]
    public class IUsersHelperTests
    {

        [TestMethod]
        public void AddUser_ShouldAddUserToDb()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase("AddUser_ShouldAddUserToDb")
            .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);

            var usersDto = new UsersDto { Username = "test-user" };

            // Act
            var result = helper.AddUser(usersDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("test-user", result.Username);
            Assert.IsTrue(context.Users.Any(u => u.Username == "test-user"));
        }

        [TestMethod]
        public void GetUser_WhenUserIdIsNull_ShouldReturnAllUsers()
        {
            // Arrange
            DbContextOptions<ApplicationContext>options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUser_WhenUserIdIsNull_ShouldReturnAllUsers")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);

            context.Users.Add(new User { UserId = 1, Username = "User1" });
            context.Users.Add(new User { UserId = 2, Username = "User2" });
            context.SaveChanges();
              

             // Act
             var result = helper.GetUser(null);

            // Assert
             Assert.IsNotNull(result);
             Assert.AreEqual(2, result.Count);
             Assert.IsTrue(result.Any(u => u.Username == "User1"));
             Assert.IsTrue(result.Any(u => u.Username == "User2"));

            
        }

        [TestMethod]
        public void GetUser_WhenUserIdIsProvided_ShouldReturnSingleUser()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUser_WhenUserIdIsProvided_ShouldReturnSingleUser")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);


             context.Users.Add(new User { UserId = 1, Username = "User1" });
             context.Users.Add(new User { UserId = 2, Username = "User2" });
             context.SaveChanges();

             // Act
             var result = helper.GetUser(1);

             // Assert
             Assert.IsNotNull(result);
             Assert.AreEqual(1, result.Count);
             Assert.AreEqual("User1", result[0].Username);
            
        }

        [TestMethod]
        public void ConnectUserToArtist_ShouldConnectUserToArtist()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ConnectUserToArtist_ShouldConnectUserToArtist")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);

            var userId = 1;
            var artistId = 101;

            context.Users.Add(new User { UserId = userId, Artists = new List<Artist>() });
            context.Artists.Add(new Artist { ArtistId = artistId });
            context.SaveChanges();


            //Act
            var result = helper.ConnectUserToArtist(userId, artistId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(artistId, result.Artists.Single().ArtistId);

        }

        [TestMethod]
        public void ConnectUserToGenre_ShouldConnectUserToGenre()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ConnectUserToGenre_ShouldConnectUserToGenre")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);

            var userId = 1;
            var genreId = 10;

            context.Users.Add(new User { UserId = userId, Artists = new List<Artist>() });
            context.Genres.Add(new Genre { GenreId = genreId });
            context.SaveChanges();


            //Act
            var result = helper.ConnectUserToGenre(userId, genreId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(genreId, result.Genres.Single().GenreId);


        }
        [TestMethod]
        public void ConnectUserToSong_ShouldConnectUserToSong()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ConnectUserToSong_ShouldConnectUserToSong")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IUsersHelper helper = new UsersHelper(context);

            var userId = 1;
            var songId = 100;

            context.Users.Add(new User { UserId = userId, Artists = new List<Artist>() });
            context.Songs.Add(new Song { SongId = songId });
            context.SaveChanges();


            //Act
            var result = helper.ConnectUserToSong(userId, songId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(songId, result.Songs.Single().SongId);

        }

    }
}
