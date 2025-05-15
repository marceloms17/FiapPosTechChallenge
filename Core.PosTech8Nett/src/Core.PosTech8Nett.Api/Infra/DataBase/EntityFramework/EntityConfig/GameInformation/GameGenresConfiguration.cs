using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.GameInformation
{
    public class GameGenresConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.ToTable("GMS_GameGenres");

            builder.HasKey(gg => new { gg.IdGame, gg.IdGenre });

            builder.HasOne(gg => gg.Game)
                   .WithMany(g => g.Genres)
                   .HasForeignKey(gg => gg.IdGame);

            builder.HasOne(gg => gg.GenreType)
                   .WithMany(gt => gt.GameGenres)
                   .HasForeignKey(gg => gg.IdGenre);
        }
    }
}
