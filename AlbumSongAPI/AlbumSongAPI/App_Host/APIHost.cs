using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using SongAPI.Interfaces.Provider;
using SongAPI.Interfaces.Repository;
using SongAPI.Provider;
using SongAPI.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlbumSongAPI.App_Host
{
    public class APIHost : AppHostBase
    {
        public APIHost()
            : base("MusicService", typeof(AlbumService).Assembly)
        {

        }

        public override void Configure(Funq.Container container)
        {
            #region Common Objects Registration

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            //Set JSON web services to return idiomatic JSON camelCase properties
            JsConfig.EmitCamelCaseNames = true;

            //Configure Logger
            log4net.Config.XmlConfigurator.Configure();
            ILogFactory _logFactory = new Log4NetFactory(true);
            ILog _logger = _logFactory.GetLogger(this.GetType());
            container.Register<ILog>(_logger);

            //Configure Connection Factory Provider
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            IDbConnectionFactory _dbConnectionFactory = new OrmLiteConnectionFactory(ConnectionString, SqlServerOrmLiteDialectProvider.Instance);
            container.Register<IDbConnectionFactory>(_dbConnectionFactory);

            #endregion

            #region Repository Registration

            //Configure repositories
            IAlbumRepository _albumRepository = new AlbumRepository(_dbConnectionFactory, _logger);
            ISongRepository _songRepository = new SongRepository(_dbConnectionFactory, _logger);
            container.Register<IAlbumRepository>(_albumRepository);
            container.Register<ISongRepository>(_songRepository);

            #endregion

            #region Provider Registration

            //Configure providers
            IAlbumProvider _albumProvider = new AlbumProvider(_albumRepository, _logger);
            ISongProvider _songProvider = new SongProvider(_songRepository, _logger);
            container.Register<IAlbumProvider>(_albumProvider);
            container.Register<ISongProvider>(_songProvider);

            #endregion
        }

    }
}