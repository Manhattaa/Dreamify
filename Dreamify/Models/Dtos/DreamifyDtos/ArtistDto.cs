namespace Dreamify.Models.Dtos.DreamifyDtos
{
    public class ArtistDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpotifyArtistId { get; set; }
        public int Popularity { get; set; }

        public ICollection<string> Genre { get; set; }
    }
}
