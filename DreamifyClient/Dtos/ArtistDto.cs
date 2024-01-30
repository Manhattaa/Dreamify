using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient.Dtos
{
    public class ArtistDto
    {

        public string ArtistName { get; set; }
        public string? Description { get; set; }
        public string? SpotifyArtistId { get; set; }
        public int? Popularity { get; set; }
        public string? Genre { get; set; }
    }
}
