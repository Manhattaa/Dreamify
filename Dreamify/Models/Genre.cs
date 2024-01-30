using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<User>? Users { get;}
        public virtual ICollection<Song>? Songs { get;}
    }
}
