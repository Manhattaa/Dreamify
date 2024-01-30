namespace Dreamify.Models.Dtos.SpotifyDtos.Artists
{
    public class AddSpotifyArtistDto
    {
        public int UserId { get; set; }
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Description { get; set; }
        public int Popularity { get; set; }
        public string Title { get; set; }
        public int Followers { get; set; }
    }
}
