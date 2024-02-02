using Dreamify.Data;
using Dreamify.Models;
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
    public class IArtistsHelperTests
    {
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


    }
}