using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient.Dtos
{
    public class SpotifySongDto
    {
        public int UserId { get; set; }
        public string SongName { get; set; }
        public string SpotifySongId { get; set; }
        public string ArtistName { get; set; }
        public string SpotifyArtistId { get; set; }
    }
}
