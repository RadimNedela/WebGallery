using Microsoft.Extensions.DependencyInjection;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.DBModel.Factories;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.IoC
{
    public static class DomainServicesInstaller
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDatabaseFactory, DatabaseFactory>();
            serviceCollection.AddSingleton<IDatabaseInfoDBFactory, DatabaseInfoDBFactory>();
            serviceCollection.AddSingleton<IRackFactory, RackFactory>();
            serviceCollection.AddSingleton<IRootPathFactory, FileSystemRootPathFactory>();
            serviceCollection.AddSingleton<IDirectoryReader, DirectoryMethods>();
            serviceCollection.AddSingleton<IFileReader, FileMethods>();
            serviceCollection.AddSingleton<IHasher, Sha1Hasher>();

            return serviceCollection;
        }
    }
}
