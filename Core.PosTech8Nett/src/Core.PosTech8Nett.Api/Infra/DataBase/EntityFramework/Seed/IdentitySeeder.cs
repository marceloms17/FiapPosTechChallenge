using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
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
            UserManager<UsersEntitie> userManager,
            RoleManager<Roles> roleManager,
            ILogger logger,
            IConfiguration configuration)
        {
            var adminEmail = configuration["SeedUser:AdminEmail"] ?? "admin@admin.com";
            var adminPassword = configuration["SeedUser:AdminPassword"] ?? throw new Exception("Senha do admin não configurada");

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new UsersEntitie
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

       

            var roleExists = await userManager.IsInRoleAsync(adminUser, "Admin");
            if (roleExists is false)
            {
                var role = await roleManager.FindByNameAsync("Admin");

                var userRole = new UserRoles
                {
                    UserId = adminUser.Id,
                    RoleId = role.Id
                };
              

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=sqlserver;Database=pos_tech_staging;User=sa;Password=huaHhbSyjn9bttt;TrustServerCertificate=true;MultipleActiveResultSets=true");

                using var dbContext = new ApplicationDbContext(optionsBuilder.Options);
                dbContext.UserRoles.Add(userRole);
                dbContext.SaveChanges();

                logger.LogInformation("Usuário admin adicionado à role 'Admin'.");
            }
        }
    }
}
