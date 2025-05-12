using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.PosTech8Nett.Api.Domain.Entities.UserInformation;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.UserInformation
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
              .UseIdentityColumn(1, 1)
              .ValueGeneratedOnAdd();

            builder.Property(u => u.Street)
               .HasMaxLength(200);

            builder.Property(u => u.Number);

            builder.Property(u => u.Country)
               .HasMaxLength(100);

            builder.Property(u => u.State)
               .HasMaxLength(2);

            builder.Property(u => u.ZipCode)
               .HasMaxLength(8);

            builder.Property(u => u.Complement)
               .HasMaxLength(100);

            builder.Property(u => u.CreateAt).ValueGeneratedOnAdd();

            builder.Property(u => u.UpdateAt).ValueGeneratedOnAddOrUpdate();

            builder.HasOne(a => a.User)
                   .WithOne(u => u.Address)
                   .HasForeignKey<Address>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("UAC_Address");
        }
    }
}
