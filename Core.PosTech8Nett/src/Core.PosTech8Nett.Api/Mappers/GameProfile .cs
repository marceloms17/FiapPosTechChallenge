using AutoMapper;
using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Domain.Model.Game;
using System.Linq;

namespace Core.PosTech8Nett.Api.Mappers
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameResponse>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.Genres.Select(g => g.GenreType.Description)));

            CreateMap<GameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // será tratado manualmente
        }
    }

}
