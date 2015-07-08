using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO.Request
{
    //Request class to add or update a song.
    //POST adds a song, where as PUT updates a song.
    [Route("/song/add", "POST")]
    [Route("/song/update", "PUT")]
    public class SongRequest
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public double LengthInMMSS { get; set; } //mm:ss
        public int TrackNumer { get; set; }
        public string Genre { get; set; }
        public int AlbumId { get; set; }
    }
}
