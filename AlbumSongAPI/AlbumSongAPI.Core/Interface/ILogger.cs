using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlbumSongAPI.Core.Interface
{
    public interface ILogger
    {
        void logInfo(Type type, string message);
        void logError(Type type, string message, Exception ex = null);
    }
}
