using Core.PosTech8Nett.Api.Infra.DataBase.Repository;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.PosTech8Nett.Api.Infra.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services)
        {

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            //Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
