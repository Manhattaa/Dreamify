﻿namespace Dreamify.Models.ViewModels.SpotifyViewModels
{
    public class SpotifyArtistsSearchViewModel
    {
        public string SpotifyArtistId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Popularity { get; set; }
        public int Followers {  get; set; }
        public ICollection<string> Genre { get; set; }
    }
}
