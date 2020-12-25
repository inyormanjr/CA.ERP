﻿using Bogus;
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
                Description = fake.Company.CompanyName()
            };


            var response = await _client.PostAsJsonAsync<CreateBrandRequest>("api/Brand/", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var createBranchResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createBranchResponse.Should().NotBeNull();
            createBranchResponse.Id.Should().Be(Guid.Empty);
        }
    }
}