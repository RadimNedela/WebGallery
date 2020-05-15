using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplications(this IServiceCollection services)
        {
            services.AddSingleton<IDirectoryContentApplication, DirectoryContentApplication>();
        }
    }
}