using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlbumSongAPI.Core.Interface
{
    public interface IDbConnectionFactoryProvider
    {
        IDbConnectionFactory getConnectionFactory();
    }
}
