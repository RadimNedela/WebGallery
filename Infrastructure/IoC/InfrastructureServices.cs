using System;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Infrastructure.Databases.MySqlDb;
using Infrastructure.Databases.SqlServer;
using Infrastructure.DomainImpl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class InfrastructureServices
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDirectoryMethods, DirectoryMethods>();
            services.AddSingleton<IHasher, FileHasher>();

            services.AddScoped<IContentEntityRepository, ContentEntitiesRepository>();
            services.AddScoped<IBinderEntityRepository, BinderEntitiesRepository>();
            services.AddSingleton<IDatabaseInfoEntityRepository, DatabaseInfoRepository>();

            var provider = configuration["GaleryDatabaseProvider"];
            if (provider == "MySql")
            {
                services.AddSingleton<IGaleryDatabase, MySqlDbContext>();
                services.AddDbContext<MySqlDbContext>();
            } else if (provider == "SqlServer")
            {
                services.AddSingleton<IGaleryDatabase, SqlServerDbContext>();
                services.AddDbContext<SqlServerDbContext>();
            }
            else
            {
                throw new NotImplementedException("Missing galery database provider, or the provider is not specified in the config file");
            }
        }
    }
}