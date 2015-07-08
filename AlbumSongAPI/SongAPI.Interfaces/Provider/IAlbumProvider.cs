using SongAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Interfaces.Provider
{
    //Interface for all album related business or data manipulation operations
    public interface IAlbumProvider
    {
        AlbumDTO GetAlbumInfo(string albumName);
    }
}
