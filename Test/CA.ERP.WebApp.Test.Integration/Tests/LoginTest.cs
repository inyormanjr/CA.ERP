using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class LoginTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public LoginTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        [Fact]
        public async Task ShouldLoginSuccessful()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Login", new LoginRequest() { Username = "ExistingUser", Password = "password" });


            Assert.True(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }

        [Fact]
        public async Task ShouldLoginFail()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Login", new LoginRequest() { Username = "ExistingUser", Password = "incorrectPassword" });


            Assert.False(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }
    }
}
