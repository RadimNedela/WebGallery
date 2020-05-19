using Domain.InfrastructureInterfaces;
using Infrastructure.DomainImpl;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class InfrastructureServices
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IDirectoryMethods, DirectoryMethods>();
        }
    }
}