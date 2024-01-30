using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Songs
{
    public class SongsContainer
    {
        [JsonPropertyName("items")]
        public List<SpotifySongDto> Items { get; set; }
    }
}
