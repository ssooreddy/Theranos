using ServiceStack.Logging;
using ServiceStack.ServiceInterface;
using SongAPI.DTO.Request;
using SongAPI.DTO.Response;
using SongAPI.DTO;
using SongAPI.Interfaces.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common.Web;

namespace AlbumSongAPI
{
    public class AlbumService : Service
    {
        IAlbumProvider _albumProvider = null;
        ILog _logger = null;
        //Album provider and logger are available as they are registered into the conatiner in AppHost
        public AlbumService(IAlbumProvider albumProvider,ILog logger)
        {
            _albumProvider = albumProvider;
            _logger = logger;
        }
       
        public AlbumResponse GET(AlbumRequest request)
        {
            AlbumResponse response = new AlbumResponse();
            try
            {
                response.AlbumDetails = _albumProvider.GetAlbumInfo(request.AlbumName);
                if (response.AlbumDetails == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Oops! No album present.";
                }
                else
                {
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumService : GET AlbumResponse", ex);
                throw;
            }
            return response;
        }
    }
}