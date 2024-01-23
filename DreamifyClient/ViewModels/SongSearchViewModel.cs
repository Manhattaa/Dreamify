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
        [JsonPropertyName("spotifySongId")]
        public string SpotifySongId { get; set; }

        [JsonPropertyName("songName")]
        public string SongName { get; set; }

        private string _allArtists { get; set; }

        [JsonPropertyName("artists")]
        public List<SongArtistViewModel> AllArtists 
        { 
            get { return AllArtists; } 
            set {
                List<SongArtistViewModel> _allArtists = value;

                Artist = _allArtists.First();    
            } 
        }

        public SongArtistViewModel Artist { get; set; }
    }
}
