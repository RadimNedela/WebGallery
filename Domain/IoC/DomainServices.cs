using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.IoC
{
    public static class DomainServices
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<DirectoryContentBuilder, DirectoryContentBuilder>();
        }
    }
}