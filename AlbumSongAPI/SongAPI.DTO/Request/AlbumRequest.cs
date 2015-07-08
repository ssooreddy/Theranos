using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO.Request
{
    //Request Class to get album details by name
    [Route("/album/songs", "GET")]
    [Route("/album/songs/{AlbumName}", "GET")]
    public class AlbumRequest
    {
        public string AlbumName { get; set; }
    }
}
