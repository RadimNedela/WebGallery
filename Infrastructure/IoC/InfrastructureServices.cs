using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Infrastructure.Databases.SqlServer;
using Infrastructure.DomainImpl;
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

            services.AddSingleton<IGaleryDatabase, SqlServerDbContext>();
            services.AddDbContext<SqlServerDbContext>();
        }
    }
}