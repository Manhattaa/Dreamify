using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DreamifyClient.ViewModels
{
    public class SongSearchViewModel
    {
        [JsonPropertyName("spotifyArtistId")]
        public string SpotifyArtistId { get; set; }

        [JsonPropertyName("artistName")]
        public string Name { get; set; }
    }
}
