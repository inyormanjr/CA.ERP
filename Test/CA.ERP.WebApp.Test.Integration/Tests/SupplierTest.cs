using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.Supplier;
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
            CreateSupplierRequest request = new CreateSupplierRequest() { 
                Data = new SupplierCreate() { 
                    Name = "SupplierXYX" ,
                    SupplierBrands = new List<SupplierBrandCreate>() { 
                        new SupplierBrandCreate() { BrandId = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c")}
                    }
                } 
            };
            var response = await _client.PostAsJsonAsync("api/Supplier", request);

            response.IsSuccessStatusCode.Should().BeTrue();

            var createSupplierResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createSupplierResponse.Should().NotBeNull();
            createSupplierResponse.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task ShouldCreateSupplierFail_BadRequest()
        {
            CreateSupplierRequest request = new CreateSupplierRequest() { Data = new SupplierCreate() { Name = "" } };
            var response = await _client.PostAsJsonAsync<CreateSupplierRequest>("api/Supplier", request);

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
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Data = new SupplierUpdate() { Name = "Supplier Updated" } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateSupplierFail_NotFound()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f2");
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Data = new SupplierUpdate() { Name = "Supplier Updated" } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}", request);

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldUpdateSupplierFail_BadRequest()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateSupplierRequest request = new UpdateSupplierRequest() { Data = new SupplierUpdate() { Name = "" } };
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

            var getSuppliersResponse = await response.Content.ReadAsAsync<GetManyResponse<SupplierView>>();
            getSuppliersResponse.Should().NotBeNull();
            getSuppliersResponse.Data.Should().HaveCountGreaterThan(0);
            getSuppliersResponse.Data.FirstOrDefault().Name.Should().NotBeNullOrEmpty();
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

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldUpdateSupplierMasterProductSuccess_NoContent()
        {
            var random = new Random();
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateBaseRequest<SupplierMasterProductUpdate> request = new UpdateBaseRequest<SupplierMasterProductUpdate>() { Data = new SupplierMasterProductUpdate() { MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), CostPrice = random.Next(0, 50000) } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/MasterProduct", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateSupplierMasterProductFail_NotFound()
        {
            var random = new Random();
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f0");
            UpdateBaseRequest<SupplierMasterProductUpdate> request = new UpdateBaseRequest<SupplierMasterProductUpdate>() { Data = new SupplierMasterProductUpdate() { MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), CostPrice = random.Next(0, 50000) } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/MasterProduct", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateSupplierMasterProductFail_BadRequest()
        {
            var random = new Random();
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateBaseRequest<SupplierMasterProductUpdate> request = new UpdateBaseRequest<SupplierMasterProductUpdate>() { Data = new SupplierMasterProductUpdate() { MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d19530eeee"), CostPrice = random.Next(0, 50000) } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/MasterProduct", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldUpdateSupplierBrandSuccess_NoContent()
        {
            var random = new Random();
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("9e1b807c-ddd6-43ec-b5f3-f986863f1762") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateSupplierBrandFail_NotFound()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba333");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("9e1b807c-ddd6-43ec-b5f3-f986863f1762") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldUpdateSupplierBrandFail_BadRequest()
        {
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefceeeeee") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldDeleteSupplierBrandSuccess_NoContent()
        {
            var supplierId = Guid.Parse("9b7b6268-dce4-4620-a5e4-f6ae95a4b229");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("92f6f00c-d830-4770-aebd-0e7de960c318") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldDeleteSupplierBrandFail_NoFound()
        {
            var supplierId = Guid.Parse("9b7b6268-dce4-4620-a5e4-f6ae95a4b333");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("92f6f00c-d830-4770-aebd-0e7de960c318") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteSupplierBrandFail_BadRequest()
        {
            var supplierId = Guid.Parse("9b7b6268-dce4-4620-a5e4-f6ae95a4b229");
            UpdateBaseRequest<SupplierBrandCreate> request = new UpdateBaseRequest<SupplierBrandCreate>() { Data = new SupplierBrandCreate() { BrandId = Guid.Parse("92f6f00c-d830-4770-aebd-0e7de960c333") } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/Brand", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldGetAtLeastOneSupplierBrandsSuccess_Ok()
        {
            var id = Guid.Parse("b61753af-a4bf-45c4-b507-6ab661b063ad");
            var response = await _client.GetAsync($"api/Supplier/{id}/Brands");


            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<SupplierView>>();
            content.Should().NotBeNull();
            content.Data.Should().HaveCountGreaterThan(0);
        }


        [Fact]
        public async Task ShouldGetEmptySupplierBrandsSuccess_Ok()
        {
            var id = Guid.Parse("9b7b6268-dce4-4620-a5e4-f6ae95a4b2ee");
            var response = await _client.GetAsync($"api/Supplier/{id}/Brands");


            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<SupplierView>>();
            content.Should().NotBeNull();
            content.Data.Should().HaveCount(0);
        }
    }
}
