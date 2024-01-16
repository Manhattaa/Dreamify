using Moq;
using Dreamify;
using Dreamify.Services;

namespace DreamifyTests
{
    [TestClass]
    public class AddArtistTest
    {
        [TestMethod]
        public void AddArtist_Check()
        {
            //Arrange
            var mockService = new Mock<IArtistsHelper>();
            IArtistsHelper artist = mockService.Object;
        }
    }
}