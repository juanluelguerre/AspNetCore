﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.IO;

namespace ElGuerre.AspNetCore.SampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureLogging((hosting, logging) =>
                {
                    logging.ClearProviders();

                    // 1) Default AspNetCore Configuration Log files
                    // logging.AddConfiguration(hosting.Configuration.GetSection("Logging"));
                    // logging.SetMinimumLevel(LogLevel.Warning); // Minimun level changed. By default mininum level is "Information"

                    // 2) Use NLog
                    logging.ConfigureNLog("nlog.config");
                    
                })
                // .UseNLog()  // NLog: setup NLog for Dependency injection                
                .Build();

            host.Run();
        }
    }
}
