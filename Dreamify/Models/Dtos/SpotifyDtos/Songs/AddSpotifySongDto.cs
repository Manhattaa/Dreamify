namespace Dreamify.Models.Dtos.SpotifyDtos.Songs
{
    public class AddSpotifySongDto
    {
        public int UserId { get; set; }
        public string SongName { get; set; }
        public string SpotifySongId { get; set; }
        public string ArtistName { get; set; }
        public string SpotifyArtistId { get; set; }
    }
}
