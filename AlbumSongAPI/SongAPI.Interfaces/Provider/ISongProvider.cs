using SongAPI.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Interfaces.Provider
{
    //Interface for all song related business or data manipulation operations
    public interface ISongProvider
    {
        bool AddSong(SongRequest song,out string errMsg);
        bool UpdateSong(SongRequest song, out string errMsg);
    }
}
