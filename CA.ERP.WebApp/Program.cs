using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, builder) => {
                    var localJsonPath = Path.Combine(System.AppContext.BaseDirectory, $"appsettings.{host.HostingEnvironment.EnvironmentName}.local.json");
                    if (File.Exists(localJsonPath))
                    {
                        builder.AddJsonFile(localJsonPath);
                    }
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddFile("logs/ca-erp-{Date}.log");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
