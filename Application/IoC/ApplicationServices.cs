using Application.Directories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ApplicationServices
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddSingleton<DirectoryContentApplication, DirectoryContentApplication>();
            services.AddSingleton<PhysicalFileApplication, PhysicalFileApplication>();
        }
    }
}