using SongAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Interfaces.Repository
{
    //Interface for all song related data base operations
    public interface ISongRepository
    {
        bool AddSong(Song song, out string errMsg);
        bool UpdateSong(int songId, string title, double length, string genre, int albumId, out string errMsg);
    }
}
