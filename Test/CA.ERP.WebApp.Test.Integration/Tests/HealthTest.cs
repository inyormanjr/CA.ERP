using CA.ERP.WebApp.Test.Integration.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class HealthTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public HealthTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }


        [Fact]
        public async Task ShouldReturnHealthy()
        {
            var response = await _client.GetAsync("/health");
            var health = await response.Content.ReadAsStringAsync();
            Assert.Equal("Healthy", health);
        }
    }
}
