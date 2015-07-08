using ServiceStack.Logging;
using ServiceStack.OrmLite;
using SongAPI.Domain;
using SongAPI.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace SongAPI.Repository
{
    //Repository class to handle song related database operations implemented from ISongRepository
    public class SongRepository : ISongRepository
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private ILog _logger;
        //DB Connection factory and logger are available as they are registered into the conatiner in AppHost
        public SongRepository(IDbConnectionFactory DBConnectionFactory, ILog logger)
        {
            this._dbConnectionFactory = DBConnectionFactory;
            this._logger = logger;
        }

        //function to add song to DB or XML, depending on where album is present. Calls AddSongInXML if album is not present in DB
        public bool AddSong(Song song, out string errMsg)
        {
            //Flag to check if album is presen in DB.
            bool isAlbumExist = false;
            errMsg = string.Empty;
            try
            {
                using (var dbConn = _dbConnectionFactory.OpenDbConnection())
                {
                    using (var trans = dbConn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        if (dbConn.Select<Album>(x => x.Id == song.AlbumId).Count() > 0)
                        {
                            //Marked as true as album is present
                            isAlbumExist = true;
                            song.DateModified = DateTime.Now;
                            song.DateAdded = DateTime.Now;
                            dbConn.Insert<Song>(song);
                            trans.Commit();
                        }
                    }
                }
                if (!isAlbumExist)
                {
                    //Add song to XML as album is not present in DB.
                    errMsg = AddSongInXML(song) == false ? "No album exist" : string.Empty;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumRepository.cs - GetAlbumSongs", ex);
                throw ex;
            }
        }

        //function to update song to DB or XML, depending on where album is present. Calls UpdateSongInXML if album is not present in DB
        public bool UpdateSong(int songId, string title, double length, string genre, int albumId, out string errMsg)
        {
            //Flag to check if song is present in DB.
            bool isSongExist = false;
            errMsg = string.Empty;
            try
            {
                using (var dbConn = _dbConnectionFactory.OpenDbConnection())
                {
                    using (var trans = dbConn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        isSongExist = dbConn.Update<Song>(new { Title = title, Length = length, Genre = genre }, x => x.Id == songId && x.AlbumId == albumId) > 0 ? true : false;
                    }
                }
                if (!isSongExist)
                {
                    //updates song to XML as album is not present in DB.
                    errMsg = UpdateSongInXML(songId, title, length, albumId) == false ? "No song exist" : string.Empty;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("SongRepository.cs - UpdateSong", ex);
                throw ex;
            }
        }

        public bool AddSongInXML(Song songToAdd)
        {
            try
            {
                string appdatafolder = HttpContext.Current.Server.MapPath("~/App_Data/Songs.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(appdatafolder);
                XmlNode album = doc.SelectSingleNode("music/artist/album [Id='" + songToAdd.AlbumId + "']");
                if (album != null)
                {
                    XmlElement song = doc.CreateElement("song");
                    song.SetAttribute("title", songToAdd.Title);
                    song.SetAttribute("length", songToAdd.Length + ":" + (songToAdd.Length - (int)songToAdd.Length) * 60);
                    XmlNode lastSong = album.LastChild;
                    song.SetAttribute("SongId", (Convert.ToInt32(lastSong.Attributes["SongId"]) + 1).ToString());
                    lock (this)
                    {
                        doc.Save(appdatafolder);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumRepository.cs - GetAlbumSongs", ex);
                throw ex;
            }
        }

        public bool UpdateSongInXML(int songId, string title, double length, int albumId)
        {
            try
            {
                string appdatafolder = HttpContext.Current.Server.MapPath("~/App_Data/Songs.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(appdatafolder);
                XmlNode song = doc.SelectSingleNode("music/artist/album [@Id='" + albumId + "']/song [@SongId='" + songId + "']");
                if (song != null)
                {
                    song.Attributes["title"].Value = title;
                    song.Attributes["length"].Value = length + ":" + (length - (int)length) * 60;
                    lock (this)
                    {
                        doc.Save(appdatafolder);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SongRepository.cs - GetAlbumSongs", ex);
                throw ex;
            }
        }  
    }
}
