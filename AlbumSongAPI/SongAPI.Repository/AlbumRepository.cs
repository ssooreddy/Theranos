using Dapper;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using SongAPI.Domain;
using SongAPI.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace SongAPI.Repository
{
    //Repository class to handle album related database operations implemented from IAlbumRepository
    public class AlbumRepository : IAlbumRepository
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private ILog _logger;
        //DB Connection factory and logger are available as they are registered into the conatiner in AppHost
        public AlbumRepository(IDbConnectionFactory DBConnectionFactory, ILog logger)
        {
            this._dbConnectionFactory = DBConnectionFactory;
            this._logger = logger;
        }

        //Gets album and song info from DB, XML
        public Album GetAlbumSongs(string albumName, ref List<Song> songs)
        {
            try
            {
                Album album = GetAlbumSongsFromXML(albumName, ref songs);
                using (var dbConn = _dbConnectionFactory.OpenDbConnection())
                {
                    using (var trans = dbConn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        if (album == null || album.Id == 0)
                        {
                            List<Album> albums = dbConn.Select<Album>(x => x.Title == albumName);
                            if (albums != null && albums.Count() > 0)
                                album = albums[0];
                        }
                        songs.AddRange(dbConn.Select<Song>(x => x.AlbumId == album.Id));
                    }
                }
                return album;
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumRepository.cs - GetAlbumSongs", ex);
                throw ex;
            }
        }

        //Gets album and song info from XML. This will be called from GetAlbumSongs
        public Album GetAlbumSongsFromXML(string albumName, ref List<Song> songs)
        {
            Album album = null;
            string appdatafolder = HttpContext.Current.Server.MapPath("~/App_Data/Songs.xml");
            XmlReader xmlReader = XmlReader.Create(appdatafolder);
            string artist = string.Empty;
            while (xmlReader.Read())
            {
                if (xmlReader.MoveToContent() == XmlNodeType.Element &&
                    xmlReader.Name == "artist")
                {
                    artist = xmlReader.GetAttribute("name");
                }
                if (xmlReader.MoveToContent() == XmlNodeType.Element &&
                    xmlReader.Name == "album" &&
                    String.Equals(albumName, xmlReader.GetAttribute("title"), StringComparison.InvariantCultureIgnoreCase))
                {

                    album = new Album()
                    {
                        Id = Convert.ToInt32(xmlReader.GetAttribute("Id")),
                        Title = xmlReader.GetAttribute("title"),
                        ArtistName = artist
                    };
                    if (xmlReader.ReadToDescendant("song"))
                    {
                        do
                        {
                            Song s = new Song()
                            {
                                AlbumId = album.Id.Value,
                                Id = Convert.ToInt32(xmlReader.GetAttribute("SongId")),
                                Title = xmlReader.GetAttribute("title"),
                                Length = Convert.ToDouble(xmlReader.GetAttribute("length").Split(':')[0]) + (Convert.ToDouble(xmlReader.GetAttribute("length").Split(':')[1]) / 60)
                            };
                            songs.Add(s);
                        } while (xmlReader.ReadToNextSibling("song"));
                    }
                }
            }
            return album;
        }
    }
}
