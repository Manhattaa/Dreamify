using Dreamify.Data;
using Dreamify.Models;
using Dreamify.Models.Dtos.DreamifyDtos;
using Dreamify.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyTests
{
    [TestClass]
    public class IArtistsHelperTests
    {
        [TestMethod]
        public void AddArtist_CheckIfArtistGetsAddedToDB()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddArtist_CheckIfArtistGetsAddedToDB")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistsHelper = new ArtistsHelper(context);

            // Act
            artistsHelper.AddArtist(new ArtistDto
            {
                ArtistName = "ArtistTest",
                Description = "DescriptionTest"
            });


            // Assert
            Assert.AreEqual(1, context.Artists.Count());
        }

        [TestMethod]
        public void GetArtist_CheckIfArtistGetsReturned()
        {
            //Arrange

            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetArtist_CheckIfArtistGetsReturned")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistsHelpers = new ArtistsHelper(context);


            Artist testingArtist = new Artist()
            {
                ArtistName = "ArtistTesting",
            };

            context.Artists.Add(testingArtist);
            context.SaveChanges();

            //Act 
            var result = artistsHelpers.GetArtists();


            //Assert
            Assert.AreEqual("ArtistTesting", result.First().ArtistName);
        }

        [TestMethod]
        public void GetArtist_ReturnsAllDBArtists()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetArtist_ReturnsAllDBArtists")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistsHelper = new ArtistsHelper(context);

            Artist artistTest1 = new Artist()
            {
                ArtistName = "ArtistTest1",
            };
            Artist artistTest2 = new Artist()
            {
                ArtistName = "ArtistTest2",
            };
            Artist artistTest3 = new Artist()
            {
                ArtistName = "ArtistTest3",
            };
            Artist artistTest4 = new Artist()
            {
                ArtistName = "ArtistTest4",
            };
            Artist artistTest5 = new Artist()
            {
                ArtistName = "ArtistTest5",
            };

            context.Artists.Add(artistTest1);
            context.Artists.Add(artistTest2);
            context.Artists.Add(artistTest3);
            context.Artists.Add(artistTest4);
            context.Artists.Add(artistTest5);
            context.SaveChanges();

            //act

            var result = artistsHelper.GetArtists();

            //assert
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddArtist_ThrowsExceptionWhenArtistDoesntExist()
        {
            //    // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("AddArtist_ThrowsExceptionWhenArtistDoesntExist")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistHelper = new ArtistsHelper(context);

            // Act
            artistHelper.AddArtist(new ArtistDto
            {
                ArtistName = null,
                Genres = null,
                Description = null,
            });
        }
        [TestMethod]
        public void GetUserArtists_TestIfItHooksCorrectly()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUserArtists_TestIfItHooksCorrectly")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistHelper = new ArtistsHelper(context);


            Artist artistTest1 = new Artist()
            {
                ArtistName = "TestArtist1",
                Description = "TestDescription1"
            };

            Artist artistTest2 = new Artist()
            {
                ArtistName = "TestArtist2",
                Description = "TestDescription2"
            };

            Artist artistTest3 = new Artist()
            {
                ArtistName = "TestArtist3",
                Description = "TestDescription3"
            };

            Artist artistTest4 = new Artist()
            {
                ArtistName = "TestArtist4",
                Description = "TestDescription4"
            };

            Artist artistTest5 = new Artist()
            {
                ArtistName = "TestArtist5",
                Description = "TestDescription5"
            };

            User testUser = new User()
            {
                UserId = 1,
                Username = "TestUserName",
                Artists = new List<Artist>()
                {
                    artistTest1, artistTest2, artistTest3, artistTest4, artistTest5
                }
            };

            context.Users.Add(testUser);
            context.SaveChanges();


            // Act
            var result = artistHelper.GetUserArtists(1);

            // Assert
            Assert.AreEqual(5, result.Artists.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetUserArtists_ThrowsExceptionWhenArtistIsNull()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetUserArtists_ThrowsExceptionWhenArtistIsNull")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IArtistsHelper artistHelper = new ArtistsHelper(context);


            Artist artistTest = new Artist()
            {
                ArtistName = "TestArtist",
                Description = "TestDescription"
            };

            User testUser = new User()
            {
                UserId = 1,
                Username = "UserTest",
                Artists = new List<Artist>()
                {
                    artistTest
                }
            };

            context.Users.Add(testUser);
            context.SaveChanges();


            // Act
            var result = artistHelper.GetUserArtists(2);
        }
    }
}