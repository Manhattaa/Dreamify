using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Songs
{
    public class SongSearchResponse
    {
        [JsonPropertyName("tracks")]
        public SongsContainer SongsContainer { get; set; }
        
    }
}
