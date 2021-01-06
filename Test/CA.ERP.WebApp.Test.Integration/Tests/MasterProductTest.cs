using Bogus;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.MasterProduct;
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

            var data = new MasterProductCreate() {
                Model = fake.Vehicle.Model(),
                Description = fake.Vehicle.Manufacturer(),
                BrandId = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c")
            };

            var request = new CreateMasterProductRequest()
            {
                Data = data
            };


            var response = await _client.PostAsJsonAsync("api/MasterProduct/", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createContent = await response.Content.ReadAsAsync<CreateResponse>();

            createContent.Should().NotBeNull();
            createContent.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task ShouldCreateMasterProductFail_BadRequest()
        {
            var fake = new Faker();

            var data = new MasterProductCreate()
            {
                Description = fake.Vehicle.Manufacturer(),
                BrandId = Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefce5bb2d")
            };

            var request = new CreateMasterProductRequest()
            {
                Data = data
            };


            var response = await _client.PostAsJsonAsync("api/MasterProduct/", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "it has empty model");

            var content = await response.Content.ReadAsAsync<ErrorResponse>();

            content.Should().NotBeNull();
            content.ValidationErrors.Should().Contain(error => error.PropertyName == nameof(MasterProductCreate.Model));
        }

        [Fact]
        public async Task ShouldUpdateMasterProductSuccess_NoContent()
        {
            var fake = new Faker();


            var request = new UpdateBaseRequest<MasterProductView>()
            {
                Data = new MasterProductView() {
                    Model = fake.Vehicle.Model(),
                    Description = fake.Vehicle.Manufacturer(),
                    BrandId = Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefce5bb2d")
                }
            };

            var id =  Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df3");

            var response = await _client.PutAsJsonAsync($"api/MasterProduct/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateMasterProductFail_BadRequest()
        {
            var fake = new Faker();


            var request = new UpdateBaseRequest<MasterProductView>()
            {
                Data = new MasterProductView()
                {
                    Model = fake.Vehicle.Model(),
                    Description = fake.Vehicle.Manufacturer(),
                }
            };

            var id = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df3");

            var response = await _client.PutAsJsonAsync($"api/MasterProduct/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsAsync<ErrorResponse>();
            content.Should().NotBeNull();
            content.ValidationErrors.Should().Contain(error => error.PropertyName == nameof(MasterProductCreate.BrandId));
        }

        [Fact]
        public async Task ShouldUpdateMasterProductFail_NotFound()
        {
            var fake = new Faker();


            var request = new UpdateBaseRequest<MasterProductView>()
            {
                Data = new MasterProductView()
                {
                    Model = fake.Vehicle.Model(),
                    Description = fake.Vehicle.Manufacturer(),
                    BrandId = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c")
                }
            };

            var id = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df4");

            var response = await _client.PutAsJsonAsync($"api/MasterProduct/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldGetAtleastOneMasterProduct()
        {
            var response = await _client.GetAsync("api/MasterProduct/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<MasterProductView>>();


            content.Should().NotBeNull();
            content.Data.Should().HaveCount(c => c >= 1);
        }

        [Fact]
        public async Task ShouldGetExactlyOneMasterProductSuccess()
        {
            var id = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df3");
            var response = await _client.GetAsync($"api/MasterProduct/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<MasterProductView>();


            content.Should().NotBeNull();
            content.Id.Should().Be(id);
        }

        [Fact]
        public async Task ShouldGetExactlyOneMasterProductFail_NotFound()
        {
            var id = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df4");
            var response = await _client.GetAsync($"api/MasterProduct/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

    }
}
