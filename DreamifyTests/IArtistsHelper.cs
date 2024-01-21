using Dreamify.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//This Branch will focus on Testing using EF In Memory. 
//But will be converted to Moq at a later phase.
namespace DreamifyTests
{
    [TestClass]
    public class ArtistsHelperTests
    {
        // Mocked DbContext for testing
        private Mock<ApplicationContext> _mockContext;

        [TestInitialize]
        public void Setup()
        {
            // Initialize and configure the mock DbContext
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _mockContext = new Mock<ApplicationContext>(options);
        }

        [TestMethod]
        public void AddArtist_ValidInput_ReturnsActualResult()
        {
            // Arrange
            var artistsHelper = new ArtistsHelper(_mockContext.Object);
            var genreId = 1;
            var artistDto = new ArtistDto { Name = "Test Artist", Description = "Test Description" };

            var genres = new List<Genre> { new Genre { GenreId = genreId } };

            _mockContext.Setup(c => c.GetGenres()).Returns(MockDbSet(genres));

            // Act
            var result = artistsHelper.AddArtist(genreId, artistDto, artistsHelper);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));

            _mockContext.Verify(c => c.Add(It.IsAny<Artist>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);

            var contentResult = (ContentResult)result;
            Assert.AreEqual((int)System.Net.HttpStatusCode.Created, contentResult.StatusCode);
        }

        //Helper method to mock DbSet
        private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockSet.Object;
        }
    }
}
