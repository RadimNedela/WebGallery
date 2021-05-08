using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Databases.TheDatabase.MySqlDb;
using WebGalery.Infrastructure.Databases.TheDatabase.SqlServer;
using WebGalery.Infrastructure.FileServices;
using WebGalery.Infrastructure.Repositories;

namespace WebGalery.Infrastructure.IoC
{
    public static class InfrastructureRegistration
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDirectoryMethods, DirectoryMethods>();
            services.AddSingleton<IHasher, Sha1Hasher>();

            services.AddScoped<ContentEntitiesRepository>();
            services.AddScoped<IContentEntityRepository>(x => x.GetRequiredService<ContentEntitiesRepository>());
            services.AddScoped<IPersister<Content>>(x => x.GetRequiredService<ContentEntitiesRepository>());

            services.AddScoped<BinderEntitiesRepository>();
            services.AddScoped<IBinderRepository>(x => x.GetRequiredService<BinderEntitiesRepository>());
            services.AddScoped<IPersister<Binder>>(x => x.GetRequiredService<BinderEntitiesRepository>());

            services.AddSingleton<DatabaseInfoRepository>();
            services.AddSingleton<IDatabaseInfoRepository>(x => x.GetRequiredService<DatabaseInfoRepository>());
            services.AddSingleton<IPersister<DatabaseInfo>>(x => x.GetRequiredService<DatabaseInfoRepository>());

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