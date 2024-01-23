using System.Text.Json.Serialization;

namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class SpotifyFollowersDto
    {
        [JsonPropertyName("total")]
        public int totalFollowers { get; set; }
    }
}
