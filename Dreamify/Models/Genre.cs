using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Title { get; set; }

        ICollection<User> Users { get;}
        ICollection<Song> Songs { get;}
    }
}
