using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Genre>? Genres { get; set; }
        public virtual ICollection<Artist>? Artists { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }


        public User()
        {
            Songs = new List<Song>();
            Artists = new List<Artist>();
            Genres = new List<Genre>();

        }
    }
}
    