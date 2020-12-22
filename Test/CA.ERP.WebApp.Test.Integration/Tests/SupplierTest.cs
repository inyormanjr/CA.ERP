using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class SupplierTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public SupplierTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClientWithAuthorization();
        }

        [Fact]
        public async Task ShouldCreateSupplierSuccess()
        {
            var response = await _client.PostAsJsonAsync<CreateSupplierRequest>("api/Supplier", new CreateSupplierRequest() { Name = "Supplier1" });

            var createSupplierResponse = await response.Content.ReadAsAsync<CreateSupplierResponse>();

            response.IsSuccessStatusCode.Should().BeTrue();
            createSupplierResponse.Should().NotBeNull();
            createSupplierResponse.SupplierId.Should().NotBe(Guid.Empty);
        }
    }
}
