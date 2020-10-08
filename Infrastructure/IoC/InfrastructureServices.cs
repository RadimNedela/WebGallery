using Domain.InfrastructureInterfaces;
using Infrastructure.DomainImpl;
using Infrastructure.MySqlDb;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class InfrastructureServices
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IDirectoryMethods, DirectoryMethods>();
            services.AddSingleton<IHasher, FileHasher>();

            services.AddScoped<IContentEntityRepository, ContentEntitiesRepository>();

            services.AddDbContext<MySqlDbContext>();
        }
    }
}