using System.Text.Json.Serialization;

namespace Dreamify.Models.ViewModels.SpotifyViewModels
{
    public class SongSearchViewModel
    {
        public string SpotifySongId { get; set; }
        public string SongName { get; set; }
        public ICollection<SongArtistViewModel> Artists { get; set; }
    }
}
