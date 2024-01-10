using System.ComponentModel.DataAnnotations;

namespace Dreamify.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; } 
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
    }
}
