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
    public class MasterProductTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public MasterProductTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreateMasterProductSuccess_Ok()
        {
            var fake = new Faker();


            var request = new CreateMasterProductRequest()
            {
                Model = fake.Vehicle.Model(),
                Description = fake.Vehicle.Manufacturer(),
                BrandId = Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefce5bb2d")
            };


            var response = await _client.PostAsJsonAsync("api/MasterProduct/", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createContent = await response.Content.ReadAsAsync<CreateResponse>();

            createContent.Should().NotBeNull();
            createContent.Id.Should().NotBe(Guid.Empty);
        }
    }
}
