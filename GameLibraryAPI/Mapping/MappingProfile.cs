using AutoMapper;
using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.GameDTO;
using GameLibraryAPI.Models.DTO.GenreDTO;
using GameLibraryAPI.Models.DTO.PlatformDTO;
using GameLibraryAPI.Models.DTO.ReviewDTO;

namespace GameLibraryAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PlatformName, opt => opt.MapFrom(src => src.Platform.Name));
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();

            CreateMap<Platform, PlatformDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformUpdateDto, Platform>();

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.GameTitle, opt => opt.MapFrom(src => src.Game.Title))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();
        }
    }
}
