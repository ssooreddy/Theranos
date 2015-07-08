using AutoMapper;
using ServiceStack.Logging;
using SongAPI.Domain;
using SongAPI.DTO;
using SongAPI.Interfaces.Provider;
using SongAPI.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongAPI.Provider
{
    //Implemetation of IAlbumProvider interface
    public class AlbumProvider : IAlbumProvider
    {
        private ILog _logger;
        private IAlbumRepository _albumRepository;
        //Album repository and logger are available as they are registered into the conatiner in AppHost
        public AlbumProvider(IAlbumRepository albumRepository, ILog logger)
        {
            _albumRepository = albumRepository;
            _logger = logger;
        }

        //Gets songs, album info from repository and maps to data transfer objects
        public AlbumDTO GetAlbumInfo(string albumName)
        {
            AlbumDTO albumDetails = null;
            List<Song> songs = new List<Song>();
            Album album = _albumRepository.GetAlbumSongs(albumName, ref songs);
            Mapper.CreateMap<Album, AlbumDTO>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Id));
            Mapper.CreateMap<Song, SongDTO>()
                .ForMember(dest => dest.SongId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.LengthInMMSS, opt => opt.MapFrom(src =>  Math.Round(src.Length,2)));
            if (album != null)
            {
                albumDetails = Mapper.Map<AlbumDTO>(album);
                albumDetails.Songs = Mapper.Map<List<SongDTO>>(songs);
            }
            return albumDetails;
        }
    }
}
