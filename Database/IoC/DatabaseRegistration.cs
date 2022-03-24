using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.Database.Databases;
using WebGalery.Database.Databases.TheDatabase.MySqlDb;
using WebGalery.Database.Databases.TheDatabase.SqlServer;

namespace WebGalery.Database.IoC
{
    public static class DatabaseRegistration
    {
        public static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IDirectoryMethods, DirectoryMethods>();
            //services.AddSingleton<IHasher, Sha1Hasher>();

            //services.AddScoped<ContentEntitiesRepository>();
            //services.AddScoped<IContentEntityRepository>(x => x.GetRequiredService<ContentEntitiesRepository>());
            //services.AddScoped<IPersister<Content>>(x => x.GetRequiredService<ContentEntitiesRepository>());

            //services.AddScoped<BinderEntitiesRepository>();
            //services.AddScoped<IBinderRepository>(x => x.GetRequiredService<BinderEntitiesRepository>());
            //services.AddScoped<IPersister<Binder>>(x => x.GetRequiredService<BinderEntitiesRepository>());

            //services.AddSingleton<DatabaseInfoRepository>();
            //services.AddSingleton<IDatabaseInfoRepository>(x => x.GetRequiredService<DatabaseInfoRepository>());
            //services.AddSingleton<IPersister<DatabaseInfo>>(x => x.GetRequiredService<DatabaseInfoRepository>());

            var provider = configuration["GaleryDatabaseProvider"];
            if (provider == "MySql")
            {
                services.AddSingleton<IGaleryDatabase, MySqlDbContext>();
                services.AddDbContext<MySqlDbContext>();
            }
            else if (provider == "SqlServer")
            {
                services.AddSingleton<IGaleryDatabase, SqlServerDbContext>();
                services.AddDbContext<SqlServerDbContext>();
            }
            else
                throw new NotImplementedException("Missing galery database provider, or the provider is not specified in the config file");
        }
    }
}