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
        public string ArtistName { get; set; }

        [JsonPropertyName("Popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("Followers")]
        public int Followers { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Artist")]


        private string _allArtists { get; set; }

        //[JsonPropertyName("artists")]
        //public List<ArtistGenreViewModel> AllArtists // Save only the first artist in the list
        //{
        //    get { return _allArtists; }
        //    set
        //    {
        //        _allArtists = value;
        //        Artist = _allArtists.First();
        //    }
        //}

    }
}
