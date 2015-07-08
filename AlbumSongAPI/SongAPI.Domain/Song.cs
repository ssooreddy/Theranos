using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Domain
{
    //Song table in DB
    [Serializable]
    public class Song
    {
        [AutoIncrement]
        [Alias("SongId")]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Length { get; set; }
        public int TrackNumber { get; set; }
        public string Genre { get; set; }
        public int AlbumId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
