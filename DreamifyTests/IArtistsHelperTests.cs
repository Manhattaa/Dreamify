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
        public void SpotifyArtistSearch_CheckIfArtistGetsReturned()
        {
            //Arrange

            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("SpotifyArtistSearch_CheckIfArtistGetsReturned")
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


            //Assert

        }
    }
}
