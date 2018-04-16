using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Musiction.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                var environment = webHostBuilderContext.HostingEnvironment;
                configurationbuilder
                        .AddJsonFile("appSettings.json", optional: false);


                configurationbuilder.AddEnvironmentVariables();
            })
                .UseStartup<Startup>()
                .Build();
    }
}
