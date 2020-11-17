using Application.Directories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ApplicationServices
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentApplication>();
            services.AddScoped<BinderApp>();
            services.AddScoped<DatabaseInfoApplication>();
        }
    }
}