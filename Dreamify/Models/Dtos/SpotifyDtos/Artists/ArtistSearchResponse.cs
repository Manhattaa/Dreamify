using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class ArtistSearchResponse
    {
        [JsonPropertyName("artists")]
        public ArtistContainer ArtistsContainer { get; set; }
    }
}
