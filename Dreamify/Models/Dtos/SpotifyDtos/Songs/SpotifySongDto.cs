using Dreamify.Models.Dtos.SpotifyDtos.Artists;
using Dreamify.Models.ViewModels.SpotifyViewModels;
using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Songs
{
    public class SpotifySongDto
    {
        [JsonPropertyName("id")]
        public string SpotifySongId { get; set; }

        [JsonPropertyName("name")]
        public string ArtistName { get; set; }

        [JsonPropertyName("artists")]
        public List<SongArtistDto> Artists { get; set; }
    }
}
