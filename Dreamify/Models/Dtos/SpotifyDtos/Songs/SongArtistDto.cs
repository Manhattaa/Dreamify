using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Songs
{
    public class SongArtistDto
    {
        [JsonPropertyName("id")]
        public string ArtistId { get; set; }

        [JsonPropertyName("name")]
        public string ArtistName { get; set; }
    }
}
