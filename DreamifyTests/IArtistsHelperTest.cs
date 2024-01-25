using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dreamify.Services;
using Dreamify.Handlers;
using Dreamify.Models;

namespace DreamifyTests
{
    [TestClass]
    public class IArtistsHelperTest
    {
        [TestMethod]
        public async Task SpotifyArtistSearch_CheckIfArtistViewModelsGetsReturned()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var spotifyHelper = new SpotifyHelper("clientId", "clientSecret", httpClientMock.Object);

            // moqing getaccesstoken
            httpClientMock.Setup(h => h.GetAccessToken()).ReturnsAsync("moqAccessToken");

            // mocking the response from SPOTIFY API
            var mockedResponse = new HttpResponseMessage();
            mockedResponse.Content = new StringContent(@"{
            ""artistsContainer"": {
                ""items"": [
                    {
                        ""spotifyArtistId"": ""6M2wZ9GZgrQXHCFfjv46we"",
                        ""name"": ""Dua Lipa"",
                        ""popularity"": 87,
                        ""followers"": { ""totalFollowers"": 42596502 },
                        ""genres"": [""Dance Pop"", ""Rock"", ""UK Pop""]
                    },
                    {
                        ""spotifyArtistId"": ""5x7GdOcXcuViSVCbl0hEfm"",
                        ""name"": ""Günther"",
                        ""popularity"": 42,
                        ""followers"": { ""totalFollowers"": 42008 },
                        ""genres"": [""Bubblegum Dance""]
                    }
                ]
            }
        }");
            httpClientMock.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(mockedResponse);

            // Act
            var result = await spotifyHelper.SpotifyArtistSearch("searchTerm", 0, "SE");

            // Assert
            Assert.IsNotNull(result);
            Assert.Equals(2, result.Count); // if two things are returned can be more

            // ADd more here if needed
            Assert.Equals("6M2wZ9GZgrQXHCFfjv46we", result[0].SpotifyArtistId);
            Assert.Equals("Dua Lipa", result[0].Name);
            Assert.Equals(87, result[0].Popularity);
            Assert.Equals(42596502, result[0].Followers);
            Assert.Equals(3, result[0].Genre.Count);
        }
    }
}
