﻿using Microsoft.Extensions.DependencyInjection;
using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses.Factories;

namespace WebGalery.Domain.IoC
{
    public static class DomainServicesInstaller
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDepositoryFactory, DepositoryFactory>();
            serviceCollection.AddSingleton<IDepotFactory, DepotFactory>();
            serviceCollection.AddSingleton<ILocationFactory, FileSystemLocationFactory>();
            serviceCollection.AddSingleton<IDirectoryReader, DirectoryMethods>();
            serviceCollection.AddSingleton<IFileReader, FileMethods>();
            serviceCollection.AddSingleton<IHasher, Sha1Hasher>();

            return serviceCollection;
        }
    }
}
