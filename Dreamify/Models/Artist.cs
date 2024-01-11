using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }


        public virtual Genre Genre { get; set; }
        ICollection<User> Users { get; set;}
        ICollection<Song> Songs { get; set;}
    }
}
