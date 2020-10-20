using Application.Directories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Controllers;

namespace Application.IoC
{
    public static class ApplicationServices
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentApplication, DirectoryContentApplication>();
            services.AddScoped<PhysicalFileApplication, PhysicalFileApplication>();
            services.AddScoped<DatabaseInfoApplication, DatabaseInfoApplication>();
            services.AddSingleton<IDatabaseInfoProvider, DatabaseInfoProvider>();
        }
    }
}