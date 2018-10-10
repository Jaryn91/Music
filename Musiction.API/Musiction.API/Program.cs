using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Musiction.API
{
    public class Program
    {
        private static readonly Dictionary<string, string> defaults =
            new Dictionary<string, string> { {
                    WebHostDefaults.EnvironmentKey, "development"
                } };

        private static string env;

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            env = new ConfigurationBuilder().AddCommandLine(args).Build()["env"];
            var element = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                webHostBuilderContext.HostingEnvironment.EnvironmentName = SetHostingEnvironment(args);
                configurationbuilder.AddJsonFile("appSettings.json", optional: false)
                                    .AddInMemoryCollection(defaults)
                                    .AddCommandLine(args)
                                    .AddUserSecrets<Startup>();
            })
                .UseUrls(GetUrl(args))
                .UseStartup<Startup>()
                .Build();

            return element;
        }

        private static string GetUrl(string[] args)
        {
            if (env == "dev")
                return @"http://localhost:5060";
            if (env == "prod")
                return @"http://localhost:5050";

            return null;
        }

        private static string SetHostingEnvironment(string[] args)
        {
            if (env == "prod")
                return "Production";

            return "Development";
        }
    }
}
