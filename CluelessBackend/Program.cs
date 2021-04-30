using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApplication;

namespace CluelessBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Allow args to specify port number?
            // TODO: Load server configuration file
            // TODO: Start logging

            GlobalServiceInitializer.InitializeServices();
            BackendInitializer.InitializeBackend();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<WebServerStartup>(); });
    }
}