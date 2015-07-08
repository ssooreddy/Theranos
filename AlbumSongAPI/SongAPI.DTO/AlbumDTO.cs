using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO
{
    //Data transfer object to send album details with songs
    [Serializable]
    public class AlbumDTO
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public List<SongDTO> Songs { get; set; }
    }
}
