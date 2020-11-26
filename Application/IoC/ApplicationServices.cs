using Application.Directories;
using Application.Maintenance;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ApplicationServices
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentApplication>();
            services.AddScoped<DatabaseInfoApplication>();
        }
    }
}