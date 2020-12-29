using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using CA.ERP.WebApp.Test.Integration.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class PurchaseOrderTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public PurchaseOrderTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreatePurchaseOrderSuccess()
        {
            var response = await _client.PostAsJsonAsync<CreatePurchaseOrderRequest>("api/PurchaseOrder", new CreatePurchaseOrderRequest() {  });

            response.IsSuccessStatusCode.Should().BeTrue();

            var createSupplierResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createSupplierResponse.Should().NotBeNull();
            createSupplierResponse.Id.Should().NotBe(Guid.Empty);
        }
    }
}
