namespace Dreamify.Models.Dtos.DreamifyDtos
{
    public class ArtistDto
    {
        public string ArtistName { get; set; }
        public string? Description { get; set; }
        public string? SpotifyArtistId { get; set; }
        public ICollection<Genre>? Genres { get; set; }
    }
}
