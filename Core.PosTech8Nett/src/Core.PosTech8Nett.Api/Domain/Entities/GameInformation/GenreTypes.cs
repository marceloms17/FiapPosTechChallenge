using System.Collections.Generic;

namespace Core.PosTech8Nett.Api.Domain.Entities.GameInformation
{
    public class GenreTypes
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }
    }
}