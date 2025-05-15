using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.GameInformation
{
    public class GameGenre
    {
        public int IdGenre { get; set; }
        public Guid IdGame { get; set; }

        public GenreTypes GenreType { get; set; }
        public Game Game { get; set; }
    }
}