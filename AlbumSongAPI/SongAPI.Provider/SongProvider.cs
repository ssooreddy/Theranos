using AutoMapper;
using ServiceStack.Logging;
using SongAPI.Domain;
using SongAPI.DTO.Request;
using SongAPI.Interfaces.Provider;
using SongAPI.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Provider
{
    //Implementation of ISongProvider interface
    public class SongProvider : ISongProvider
    {
        private ILog _logger;
        private ISongRepository _songRepository;
        //Song repository and logger are available as they are registered into the conatiner in AppHost
        public SongProvider(ISongRepository songRepository, ILog logger)
        {
            _songRepository = songRepository;
            _logger = logger;
        }

        //Sends song information to be saved to repository after mapping to database class
        public bool AddSong(SongRequest song, out string errMsg)
        {
            Mapper.CreateMap<SongRequest, Song>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SongId))
              .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.LengthInMMSS));
            Song songToAdd = Mapper.Map<Song>(song);
            return _songRepository.AddSong(songToAdd, out errMsg);
        }

        //Sends song information to be saved to repository
        public bool UpdateSong(SongRequest song, out string errMsg)
        {
            return _songRepository.UpdateSong(song.SongId, song.Title, song.LengthInMMSS, song.Genre, song.AlbumId, out errMsg);
        }
    }
}
