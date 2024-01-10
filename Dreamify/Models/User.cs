using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        ICollection<Genre> Genres { get; set; }
        ICollection<Artist> Artists { get; set; }
    }
}
    