using Bogus;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using CA.ERP.WebApp.Test.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class UserTest :  IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public UserTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        public async Task InitializeAsync()
        {
            await _client.AddAuthorizationHeader();
        }

        public Task DisposeAsync()
        {
            return Task.FromResult(0);
        }


        [Fact]
        public async Task ShouldRegisterSuccessful()
        {
            var response = await _client.PostAsJsonAsync("api/User/Register", new RegisterRequest() { UserName = "User1", Password = "12345", Role = UserRole.Admin, FirstName = "Firstname", LastName = "Lastname", Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") } });


            Assert.True(response.IsSuccessStatusCode, $"Invalid http status code. actual status code {response.StatusCode}");
        }

        [Fact]
        public async Task ShouldRegisterFail_EmptyUsernamePassword()
        {
            var response = await _client.PostAsJsonAsync("api/User/Register", new RegisterRequest() { UserName = "", Password = "", Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") } });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldRegisterFail_InvalidBranch()
        {
            var response = await _client.PostAsJsonAsync("api/User/Register", new RegisterRequest() { UserName = "User2", Password = "12345", Branches = new List<Guid> { Guid.NewGuid() } });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
