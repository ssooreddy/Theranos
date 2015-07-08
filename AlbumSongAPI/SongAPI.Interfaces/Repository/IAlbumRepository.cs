using SongAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Interfaces.Repository
{
    //Interface for all album related data base operations
    public interface IAlbumRepository
    {
        Album GetAlbumSongs(string albumName, ref List<Song> songs);
    }
}
