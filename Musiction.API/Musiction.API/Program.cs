﻿using Microsoft.AspNetCore;
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
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var element = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                var environment = webHostBuilderContext.HostingEnvironment;
                configurationbuilder.AddJsonFile("appSettings.json", optional: false)
                                    .AddInMemoryCollection(defaults)
                                    .AddEnvironmentVariables("ASPNETCORE_")
                                    .AddCommandLine(args)
                                    .AddUserSecrets<Startup>();
            })
                .UseUrls(new ConfigurationBuilder().AddCommandLine(args).Build()["url"])
                .UseStartup<Startup>()
                .Build();

            return element;
        }
    }
}
