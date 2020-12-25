using Bogus;
using CA.ERP.DataAccess.Entities;
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
    public class BrandTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public BrandTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreateBrandSuccess_Ok()
        {
            var fake = new Faker();


            var request = new CreateBrandRequest()
            {
                Name = fake.Company.CompanyName(),
                Description = fake.Company.CompanyName()
            };


            var response = await _client.PostAsJsonAsync<CreateBrandRequest>("api/Brand/", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createBranchResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createBranchResponse.Should().NotBeNull();
            createBranchResponse.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task ShouldCreateBrandFail_BadRequest()
        {
            var fake = new Faker();


            var request = new CreateBrandRequest()
            {
                Description = fake.Company.CatchPhrase()
            };


            var response = await _client.PostAsJsonAsync<CreateBrandRequest>("api/Brand/", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Brand name is null");

            var createBranchResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createBranchResponse.Should().NotBeNull();
            createBranchResponse.Id.Should().Be(Guid.Empty);
        }

        [Fact]
        public async Task ShouldUpdateBrandSuccess_Ok()
        {
            var fake = new Faker();


            var request = new UpdateBrandRequest()
            {
                Data = new Dto.Brand()
                {
                    Name = fake.Company.CompanyName(),
                    Description = fake.Company.CompanyName()
                }
            };

            var id = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c");

            var response = await _client.PutAsJsonAsync($"api/Brand/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateBrandFail_BadRequest()
        {
            var fake = new Faker();


            var request = new UpdateBrandRequest()
            {
                Data = new Dto.Brand()
                {
                    Description = fake.Company.CompanyName()
                }
            };

            var id = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c");

            var response = await _client.PutAsJsonAsync($"api/Brand/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var createResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            createResponse.Should().NotBeNull();
            createResponse.GeneralError.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldUpdateBrandFail_NotFound()
        {
            var fake = new Faker();


            var request = new UpdateBrandRequest()
            {
                Data = new Dto.Brand()
                {
                    Name = fake.Company.CompanyName(),
                    Description = fake.Company.CompanyName()
                }
            };

            var id = Guid.Empty;

            var response = await _client.PutAsJsonAsync($"api/Brand/{id}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
