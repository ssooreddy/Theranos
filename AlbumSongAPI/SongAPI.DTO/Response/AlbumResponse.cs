using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO.Response
{
    //Class to populate album details. AlbumDTO is data transfer object which has album details and songs.
    //Response Status will be populated for any exceptions thrown.
    [Serializable]
    public class AlbumResponse : ResponseStatus
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public AlbumDTO AlbumDetails { get; set; }

        public ResponseStatus ResponseStatus
        {
            get;
            set;
        }
    }
}
