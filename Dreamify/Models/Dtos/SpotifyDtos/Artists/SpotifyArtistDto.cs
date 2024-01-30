using Dreamify.Models.Dtos.SpotifyDtos.Songs;
using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class SpotifyArtistDto
    {
        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }

        [JsonPropertyName("name")]
        public string ArtistName { get; set; }

        [JsonPropertyName("followers")]
        public SpotifyFollowersDto Followers { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }


    }
}