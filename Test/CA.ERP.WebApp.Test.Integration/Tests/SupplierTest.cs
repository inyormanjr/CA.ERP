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
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class SupplierTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public SupplierTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreateSupplierSuccess()
        {
            var response = await _client.PostAsJsonAsync<CreateSupplierRequest>("api/Supplier", new CreateSupplierRequest() { Name = "Supplier1" });

            response.IsSuccessStatusCode.Should().BeTrue();

            var createSupplierResponse = await response.Content.ReadAsAsync<CreateSupplierResponse>();

            createSupplierResponse.Should().NotBeNull();
            createSupplierResponse.SupplierId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task ShouldCreateSupplierFail_BadRequest()
        {
            var response = await _client.PostAsJsonAsync<CreateSupplierRequest>("api/Supplier", new CreateSupplierRequest() { Name = "" });

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var createSupplierErrorResponse = await response.Content.ReadAsAsync<ErrorResponse>();


            createSupplierErrorResponse.Should().NotBeNull();
            createSupplierErrorResponse.GeneralError.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldUpdateSupplierSuccess_NoContent()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Supplier = new Supplier() { Name = "Supplier Updated" } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateSupplierFail_NotFound()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f2");
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Supplier = new Supplier() { Name = "Supplier Updated" } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}", request);

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldUpdateSupplierFail_BadRequest()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Supplier = new Supplier() { Name = "" } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}", request);

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldGetMultipleSuppliersSuccess_Ok()
        {
            var response = await _client.GetAsync($"api/Supplier");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var getSuppliersResponse = await response.Content.ReadAsAsync<GetSuppliersResponse>();
            getSuppliersResponse.Should().NotBeNull();
            getSuppliersResponse.Suppliers.Should().HaveCountGreaterThan(0);
            getSuppliersResponse.Suppliers.FirstOrDefault().Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetOneSupplierSuccessOk()
        {
            var id = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            var response = await _client.GetAsync($"api/Supplier/{id}");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldGetOneSupplierFail_NotFound()
        {
            var id = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f3");
            var response = await _client.GetAsync($"api/Supplier/{id}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
