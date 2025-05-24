using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Seed
{
    public static class ApplicationSeeder
    {
        public static async Task SeedIdentityAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Seed");
            var configuration = services.GetRequiredService<IConfiguration>();
            var roleManager = services.GetRequiredService<RoleManager<Roles>>();
            var userManager = services.GetRequiredService<UserManager<UsersEntitie>>();

            await IdentitySeeder.SeedRolesAsync(roleManager, logger);
            await IdentitySeeder.SeedAdminUserAsync(userManager, roleManager, logger, configuration);
        }
    }
}
