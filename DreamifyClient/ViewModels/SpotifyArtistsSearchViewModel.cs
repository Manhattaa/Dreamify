using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DreamifyClient.ViewModels
{
    public class SpotifyArtistsSearchViewModel
    {
        [JsonPropertyName("spotifyArtistId")]
        public string SpotifyArtistId { get; set; }

        [JsonPropertyName("artistName")]
        public string Name { get; set; }

        public int Popularity { get; set; }
        public int Followers {  get; set; }
        public string Title { get; set; }

        private string _allArtists { get; set; }

        [JsonPropertyName("artists")]
        public List<ArtistGenreViewModel> AllArtists // Save only the first artist in the list
        {
            get { return _allArtists; }
            set
            {
                _allArtists = value;
                Artist = _allArtists.First();
            }
        }

        public ArtistGenreViewModel Artist { get; set; }
        public ArtistGenreViewModel Genre { get; set; }
    }
}
