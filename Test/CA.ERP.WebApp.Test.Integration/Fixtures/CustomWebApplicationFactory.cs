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

namespace CA.ERP.WebApp.Test.Integration.Fixtures
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup:class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("DISABLE_SPA", "true");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CADataContext>));

                services.Remove(descriptor);

                string dbName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                services.AddDbContext<CADataContext>(options =>
                {
                    
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CADataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
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
        public HttpClient CreateClientWithAuthorization()
        {
            var client = CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            var response = client.PostAsJsonAsync("api/Authentication/Login", new LoginRequest() { Username = "ExistingUser", Password = "password" }).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = response.Content.ReadAsAsync<LoginResponse>().GetAwaiter().GetResult();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginResponse.token}");
            }
            return client;
        }


    }
}
