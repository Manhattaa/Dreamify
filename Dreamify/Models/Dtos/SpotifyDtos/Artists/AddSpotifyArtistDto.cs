using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class AddSpotifyArtistDto
    { 
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("spotifyArtistId")]
        public string SpotifyArtistId { get; set; }

        [JsonPropertyName("artistName")]
        public string ArtistName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("genres")]
        public List<string>? Genres { get; set; }

        [JsonPropertyName("popularity")]
        public int? Popularity { get; set; }

        [JsonPropertyName("followers")]
        public int? Followers { get; set; }

    }
}
