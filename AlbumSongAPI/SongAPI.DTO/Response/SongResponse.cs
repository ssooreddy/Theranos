using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.DTO.Response
{
    //Return object to show whether the song is added/updated.
    public class SongResponse : IHasResponseStatus
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ResponseStatus ResponseStatus
        {
            get;
            set;
        }
    }
}
