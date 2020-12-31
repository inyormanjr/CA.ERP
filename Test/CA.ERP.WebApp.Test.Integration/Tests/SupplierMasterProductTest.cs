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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class SupplierMasterProductTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public SupplierMasterProductTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldUpdateSupplierMasterProductSuccess_NoContent()
        {
            var random = new Random();
            var supplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");
            UpdateBaseRequest<SupplierMasterProductUpdate> request = new UpdateBaseRequest<SupplierMasterProductUpdate>() { Data = new SupplierMasterProductUpdate() {MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), CostPrice = random.Next(0, 50000) } };
            var response = await _client.PutAsJsonAsync($"api/Supplier/{supplierId}/SupplierMasterProduct", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

    }
}
