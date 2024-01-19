using Dreamify.Models.Dtos.SpotifyDtos.Songs;
using System.Text.Json.Serialization;

namespace Dreamify.Models.ViewModels.SpotifyViewModels
{
    public class SongArtistViewModel
    {
        public string ArtistId { get; set; }
        public string ArtistName { get; set; }

        public SongArtistViewModel(SongArtistDto artistDto)
        {
            ArtistId = artistDto.ArtistId;
            ArtistName = artistDto.ArtistName;
        }
    }
}
