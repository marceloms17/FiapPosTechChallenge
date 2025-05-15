using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.Identity;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.UserInformation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.EntityConfig.GameInformation;


namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context
{
    /// <summary>  
    /// Represents the application's database context, providing access to the database and its entities.  
    /// </summary>  
    public class ApplicationDbContext : IdentityDbContext<
    Users,
    Roles,
    Guid,
    UserClaims,
    UserRoles,
    UserLogins,
    Claims,
    UserToken>
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.  
        /// </summary>  
        /// <param name="options">The options to configure the database context.</param>  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityRoleClaimConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserClaimConfiguration());
            builder.ApplyConfiguration(new IdentityUserConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserTokenConfiguration());

            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new ContactConfiguration());

            builder.ApplyConfiguration(new GameConfiguration());
            builder.ApplyConfiguration(new GenreTypesConfiguration());
            builder.ApplyConfiguration(new GameGenresConfiguration());


        }
    }
}
