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
            var spotifyHelper = new SpotifyHelper(httpClientMock.Object);

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
                        ""spotifyArtistId"": """",
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
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // if two things are returned can be more

            // ADd more here if needed
            Assert.Equal("123", result[0].SpotifyArtistId);
            Assert.Equal("Artist1", result[0].Name);
            Assert.Equal(80, result[0].Popularity);
            Assert.Equal(1000, result[0].Followers);
            Assert.Equal(2, result[0].Genre.Count);
        }
    }
}
