using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<User> Users { get; set;}
        public ICollection<Song> Songs { get; set;}
    }
}
