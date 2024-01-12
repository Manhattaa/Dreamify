using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
    