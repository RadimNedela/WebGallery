using Microsoft.Extensions.DependencyInjection;
using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses.Factories;

namespace WebGalery.Domain.IoC
{
    public static class DomainServicesInstaller
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DepositoryFactory>();
            serviceCollection.AddSingleton<FileSystemDepotFactory>();
            serviceCollection.AddSingleton<IDirectoryReader, DirectoryMethods>();
            serviceCollection.AddSingleton<IFileReader, FileMethods>();
            serviceCollection.AddSingleton<IHasher, Sha1Hasher>();

            return serviceCollection;
        }
    }
}
