using AutoMapper;
using GamesApi.Data;
using GamesApi.Data.Entities;
using GamesApi.Models;
using GamesApi.Models.Responses;

namespace GamesApi.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameModel, GameEntity>().ReverseMap();
            CreateMap<GetByPageResponse, PagingDataResult>().ReverseMap();
        }
    }
}
