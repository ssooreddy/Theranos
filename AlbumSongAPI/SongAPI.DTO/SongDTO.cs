using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO
{
    //Data transfer object of Song
    public class SongDTO
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public double LengthInMMSS { get; set; } //mm:ss
        public int TrackNumber { get; set; }
        public string Genre { get; set; }
    }
}
