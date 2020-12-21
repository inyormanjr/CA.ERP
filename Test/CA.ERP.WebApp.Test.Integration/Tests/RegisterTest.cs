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
            var response = await _client.PostAsJsonAsync("api/Authentication/Register", new RegisterRequest() { UserName = "User1", Password = "12345", Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") } });


            Assert.True(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }

        [Fact]
        public async Task ShouldRegisterFail_EmptyUsernamePassword()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Register", new RegisterRequest() { UserName = "", Password = "", Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") } });


            Assert.False(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }

        [Fact]
        public async Task ShouldRegisterFail_InvalidBranch()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Register", new RegisterRequest() { UserName = "User2", Password = "12345", Branches = new List<Guid> { Guid.NewGuid() } });


            Assert.False(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }
    }
}
