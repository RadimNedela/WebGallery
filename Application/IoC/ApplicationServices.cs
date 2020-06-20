using Application.Directories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ApplicationServices
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentApplication, DirectoryContentApplication>();
            services.AddScoped<PhysicalFileApplication, PhysicalFileApplication>();
        }
    }
}