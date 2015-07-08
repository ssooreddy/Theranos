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

namespace AlbumSongAPI
{
    public class SongService : Service
    {
        ISongProvider _songProvider = null;
        ILog _logger = null;
        //Song provider and logger are available as they are registered into the conatiner in AppHost
        public SongService(ISongProvider songProvider, ILog logger)
        {
            _songProvider = songProvider;
            _logger = logger;
        }

        public SongResponse POST(SongRequest request)
        {
            SongResponse response = new SongResponse();
            try
            {
                string errMsg = string.Empty;
                response.IsSuccess = _songProvider.AddSong(request, out errMsg);
                response.ErrorMessage = errMsg;
            }
            catch (Exception ex)
            {
                _logger.Error("SongService : POST SongRequest", ex);
                throw;
            }
            return response;
        }

        public SongResponse PUT(SongRequest request)
        {
            SongResponse response = new SongResponse();
            try
            {
                string errMsg = string.Empty;
                response.IsSuccess = _songProvider.UpdateSong(request, out errMsg);
                response.ErrorMessage = errMsg;
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumService : PUT SongRequest", ex);
                throw;
            }
            return response;
        }
    }
}