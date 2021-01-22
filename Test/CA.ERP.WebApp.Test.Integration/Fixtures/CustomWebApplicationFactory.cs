using CA.ERP.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CA.ERP.Domain.Helpers;
using System.Net.Http;
using CA.ERP.WebApp.Dto;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using System.IO;

namespace CA.ERP.WebApp.Test.Integration.Fixtures
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup:class
    {


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            Environment.SetEnvironmentVariable("DISABLE_SPA", "true");

            var configRoot = GetConfiguration();

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CADataContext>));

                services.Remove(descriptor);
                var dbtype = configRoot.GetValue<string>("DbType");

                if (dbtype == "SqlServer")
                {
                    string dbSuffix = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                    services.AddDbContext<CADataContext>(options =>
                    {
                        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configRoot.GetConnectionString("DefaultConnection"));
                        sqlConnectionStringBuilder.InitialCatalog = sqlConnectionStringBuilder.InitialCatalog + "-" + dbSuffix;
                        options.UseSqlServer(sqlConnectionStringBuilder.ToString(), x => x.MigrationsAssembly("CA.ERP.DataAccess"));
                    });
                }
                else
                {
                    services.AddDbContext<CADataContext>(options =>
                    {

                        options.EnableSensitiveDataLogging();
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                }
                
                

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CADataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    

                    try
                    {
                        if (db.Database.IsRelational())
                        {
                            db.Database.Migrate();
                        }else
                        {
                            db.Database.EnsureCreated();
                        }
                        
                        Utilities.InitializeDbForTests(db, scopedServices.GetService<PasswordManagementHelper>());


                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        public static IConfigurationRoot GetConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("testsettings.json");
            if (File.Exists("testsettings.local.json"))
            {
                configurationBuilder.AddJsonFile("testsettings.local.json");
            }
            return configurationBuilder.Build();
        }


    }
}
