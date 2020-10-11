using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(/*builder => builder.AddLog4Net().SetMinimumLevel(LogLevel.Debug)*/
                    logging =>
                    logging.ClearProviders().AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug))
                .ConfigureWebHostDefaults(webBuilder =>
                {

            webBuilder.UseStartup<Startup>();
        });
    }
}
