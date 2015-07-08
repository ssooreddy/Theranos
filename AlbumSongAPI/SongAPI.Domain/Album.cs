using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Domain
{
    //Album table in DB
    [Serializable]
    public class Album
    {
        [AutoIncrement]
        [Alias("AlbumId")]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
    }
}
