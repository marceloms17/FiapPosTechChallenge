using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<Roles> roleManager, ILogger logger)
        {
            string[] roles = new[] { "Admin", "Usuario" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new Roles
                    {
                        Id = Guid.NewGuid(),
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });

                    if (result.Succeeded)
                        logger.LogInformation($"Role '{role}' criada com sucesso.");
                    else
                        logger.LogError($"Erro ao criar role '{role}': {string.Join(", ", result.Errors)}");
                }
            }
        }

        public static async Task SeedAdminUserAsync(
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            ILogger logger,
            IConfiguration configuration)
        {
            var adminEmail = configuration["SeedUser:AdminEmail"] ?? "admin@admin.com";
            var adminPassword = configuration["SeedUser:AdminPassword"] ?? throw new Exception("Senha do admin não configurada");

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new Users
                {
                    Id = Guid.NewGuid(),
                    UserName = adminEmail,
                    NormalizedUserName = adminEmail.ToUpper(),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    logger.LogInformation("Usuário admin criado com sucesso.");
                }
                else
                {
                    logger.LogError($"Erro ao criar usuário admin: {string.Join(", ", result.Errors)}");
                    return;
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogInformation("Usuário admin adicionado à role 'Admin'.");
            }
        }
    }
}
