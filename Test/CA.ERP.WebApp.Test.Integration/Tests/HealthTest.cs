using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class HealthTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public HealthTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
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
