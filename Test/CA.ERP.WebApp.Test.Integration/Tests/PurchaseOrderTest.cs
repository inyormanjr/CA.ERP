using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.PurchaseOrder;
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
            PurchaseOrderCreate data = new PurchaseOrderCreate() {
                BranchId = Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4"),
                DeliveryDate = DateTime.Now.AddDays(1),
                SupplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1"),
                PurchaseOrderItems = new List<PurchaseOrderItemCreate>() { 
                    new PurchaseOrderItemCreate() { MasterProductId = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df3"), OrderedQuantity  = 10, FreeQuantity = 1, CostPrice = 1000, Discount = 50 },
                    new PurchaseOrderItemCreate() { MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), OrderedQuantity  = 5, FreeQuantity = 2, CostPrice = 950, Discount = 100 }
                }
            };

            var request = new CreatePurchaseOrderRequest()
            {
                Data = data
            };
            var response = await _client.PostAsJsonAsync("api/PurchaseOrder", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createSupplierResponse = await response.Content.ReadAsAsync<CreateResponse>();

            createSupplierResponse.Should().NotBeNull();
            createSupplierResponse.Id.Should().NotBe(Guid.Empty);
        }
    }
}
