namespace Dreamify.Models.ViewModels.SpotifyViewModels
{
    public class SpotifyArtistsSearchViewModel
    {
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public int Popularity { get; set; }
        public int Followers {  get; set; }
        public List<string> Genre { get; set; }
    }
}
