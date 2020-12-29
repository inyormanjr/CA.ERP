﻿using Bogus;
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
            var response = await _client.PostAsJsonAsync("api/User/", new RegisterRequest() { UserName = "User1asd", Password = "12345", Role = UserRole.Admin, FirstName = "Firstname", LastName = "Lastname", Branches = new List<Guid> { Guid.Parse("e80554e8-e7b5-4f8c-8e59-9d612d547d02") } });
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldRegisterFail_EmptyUsernamePassword()
        {
            var response = await _client.PostAsJsonAsync("api/User/", new RegisterRequest() { UserName = "", Password = "", Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") } });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldRegisterFail_InvalidBranch()
        {
            var response = await _client.PostAsJsonAsync("api/User/", new RegisterRequest() { UserName = "User2", Password = "12345", Branches = new List<Guid> { Guid.NewGuid() } });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldUpdateSuccessful()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c5");
            var response = await _client.PutAsJsonAsync($"api/User/{id}", new UpdateBaseRequest<UserUpdate> {
                Data = new UserUpdate() {
                    UserName = "User1",
                    Password = "12345",
                    Role = UserRole.Cashier,
                    FirstName = "Firstname",
                    LastName = "Lastname",
                    Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") }
                }
            });


            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            
        }

        [Fact]
        public async Task ShouldUpdateFail_BadRequest()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c5");
            var response = await _client.PutAsJsonAsync($"api/User/{id}", new UpdateBaseRequest<UserUpdate>
            {
                Data = new UserUpdate()
                {
                    UserName = "",
                    Password = "12345",
                    Role = UserRole.Cashier,
                    FirstName = "Firstname",
                    LastName = "Lastname",
                    Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") }
                }
            });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task ShouldUpdateFail_NotFound()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c9");
            var response = await _client.PutAsJsonAsync($"api/User/{id}", new UpdateBaseRequest<UserUpdate>
            {
                Data = new UserUpdate()
                {
                    UserName = "",
                    Password = "12345",
                    Role = UserRole.Cashier,
                    FirstName = "Firstname",
                    LastName = "Lastname",
                    Branches = new List<Guid> { Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4") }
                }
            });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task ShouldUpdatePasswordSuccessful()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c5");
            var response = await _client.PutAsJsonAsync($"api/User/{id}/Password", new PasswordUpdateRequest()
            {
                Password = "@vs2223",
                ConfirmPassword = "@vs2223"
            });


            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task ShouldUpdatePasswordFail_BadRequest()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c5");
            var response = await _client.PutAsJsonAsync($"api/User/{id}/Password", new PasswordUpdateRequest()
            {
                Password = "@vs2223",
                ConfirmPassword = "@vs2223s"
            });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task ShouldUpdatePasswordFail_NotFound()
        {
            var id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c8");
            var response = await _client.PutAsJsonAsync($"api/User/{id}/Password", new PasswordUpdateRequest()
            {
                Password = "@vs2223",
                ConfirmPassword = "@vs2223s"
            });


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task ShouldDeleteSuccessful()
        {
            var id = Guid.Parse("e02fbc42-a8dc-4359-bfa1-7f0774bd1fd4");
            var response = await _client.DeleteAsync($"api/User/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task ShouldDeleteFail_NotFound()
        {
            var id = Guid.Parse("e02fbc42-a8dc-4359-bfa1-7f0774bd1fd5");
            var response = await _client.DeleteAsync($"api/User/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task ShouldGetOneUserSuccess_Ok()
        {
            var id = Guid.Parse("9e3205dc-f63d-49b3-bbc3-67ccf15e3ffa");
            var response = await _client.GetAsync($"api/User/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldGetOneUserFail_NotFound()
        {
            var id = Guid.Parse("9e3205dc-f63d-49b3-bbc3-67ccf15e3ffa");
            var response = await _client.GetAsync($"api/User/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task ShouldGetAtLeastOneUserSuccess_Ok()
        {
            var response = await _client.GetAsync($"api/User/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<Dto.GetManyResponse<Dto.User>>();
            content.Should().NotBeNull();
            content.Data.Should().HaveCountGreaterOrEqualTo(1);
        }
    }
}