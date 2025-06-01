using AutoMapper;
using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Domain.Model.Game;
using System.Linq;

namespace Core.PosTech8Nett.Api.Domain.Mappers
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameResponse>();

            CreateMap<GameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // será tratado manualmente
        }
    }

}
