using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Core.PosTech8Nett.Api.Infra.Migration.Extension
{
    [ExcludeFromCodeCoverage]
    public static class MigrationExtensions
    {
        public static void ExecuteMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
