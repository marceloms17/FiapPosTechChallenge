using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.GameInformation
{
    public class GenreTypesConfiguration : IEntityTypeConfiguration<GenreTypes>
    {
        public void Configure(EntityTypeBuilder<GenreTypes> builder)
        {
            builder.ToTable("GMS_GenreTypes");

            builder.HasKey(gt => gt.Id);

            builder.Property(gt => gt.Description)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(gt => gt.GameGenres)
                   .WithOne(gg => gg.GenreType)
                   .HasForeignKey(gg => gg.IdGenre);
        }
    }
}
