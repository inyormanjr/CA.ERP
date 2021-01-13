using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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


            response.IsSuccessStatusCode.Should().BeTrue();
            var loginResponse =await response.Content.ReadAsAsync<LoginResponse>();
            loginResponse.Should().NotBeNull();
            var token = loginResponse.token;
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            userIdClaim.Should().NotBeNull();
            userIdClaim.Value.Should().NotBe(Guid.Empty.ToString());

            var usernameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "Username");
            usernameClaim.Should().NotBeNull();
            usernameClaim.Value.Should().Be("ExistingUser");

            var roleClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "RoleInt");
            roleClaim.Should().NotBeNull();
            roleClaim.Value.Should().Be("1");


            var firsNameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "FirstName");
            firsNameClaim.Should().NotBeNull();
            firsNameClaim.Value.Should().Be("Existing");

            var lastNameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "LastName");
            lastNameClaim.Should().NotBeNull();
            lastNameClaim.Value.Should().Be("User");
        }

        [Fact]
        public async Task ShouldLoginFail()
        {
            var response = await _client.PostAsJsonAsync("api/Authentication/Login", new LoginRequest() { Username = "ExistingUser", Password = "incorrectPassword" });

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
