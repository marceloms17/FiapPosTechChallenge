using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.Identity
{
    public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<Claims>
    {
        public void Configure(EntityTypeBuilder<Claims> builder)
        {
            builder.ToTable("UAC_Claims");
        }
    }
}
