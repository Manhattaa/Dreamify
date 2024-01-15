using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get;}
        public ICollection<Song> Songs { get;}
    }
}
