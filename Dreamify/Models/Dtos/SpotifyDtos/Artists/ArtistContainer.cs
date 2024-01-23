using System.Text.Json.Serialization;
using Dreamify.Models.Dtos.DreamifyDtos;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class ArtistContainer
    {
        [JsonPropertyName("items")]
        public List<SpotifyArtistDto> Items { get; set; }
    }
}
