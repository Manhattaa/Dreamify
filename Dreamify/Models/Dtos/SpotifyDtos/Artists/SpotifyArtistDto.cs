using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class SpotifyArtistDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }
    }
}
