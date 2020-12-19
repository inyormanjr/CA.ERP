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
    public class RegisterTest :  IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public RegisterTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }


        [Fact]
        public async Task ShouldRegisterSuccessful()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Register", new RegisterRequest() { UserName = "User1", Password = "12345", BranchId = 1 });


            Assert.True(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }

        [Fact]
        public async Task ShouldRegisterFail_EmptyUsernamePassword()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Register", new RegisterRequest() { UserName = "", Password = "", BranchId = 1 });


            Assert.False(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }
    }
}
