using CA.ERP.DataAccess;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.PurchaseOrder;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using CA.ERP.WebApp.Test.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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

            var content = await response.Content.ReadAsAsync<CreateResponse>();

            content.Should().NotBeNull();
            content.Id.Should().NotBe(Guid.Empty);

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();

                foreach (var purchaseOrderItem in data.PurchaseOrderItems)
                {
                    var actualPrice = dbContext.SupplierMasterProducts.Where(smp => smp.SupplierId == data.SupplierId && smp.MasterProductId == purchaseOrderItem.MasterProductId).Select(smp => smp.CostPrice).FirstOrDefault();
                    actualPrice.Should().Be(purchaseOrderItem.CostPrice);
                }
            }
        }

        [Fact]
        public async Task ShouldCreatePurchaseOrderFail_WrongBranch()
        {
            PurchaseOrderCreate data = new PurchaseOrderCreate()
            {
                BranchId = Guid.Parse("e80554e8-e7b5-4f8c-8e59-9d612d547d02"),
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

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task ShouldCreatePurchaseOrderFail_EmptyBranchId_BadRequest()
        {
            PurchaseOrderCreate data = new PurchaseOrderCreate()
            {
                BranchId = Guid.Empty,
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

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Branch Id is empty");

            var content = await response.Content.ReadAsAsync<ErrorResponse>();

            content.Should().NotBeNull();
            content.ValidationErrors.Should().Contain(e => e.PropertyName == nameof(PurchaseOrderCreate.BranchId));

            
        }

        [Fact]
        public async Task ShouldCreatePurchaseOrderFail_EmptySupplierId_BadRequest()
        {
            PurchaseOrderCreate data = new PurchaseOrderCreate()
            {
                BranchId = Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4"),
                DeliveryDate = DateTime.Now.AddDays(1),
                SupplierId = Guid.Empty,
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

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Branch Id is empty");

            var content = await response.Content.ReadAsAsync<ErrorResponse>();

            content.Should().NotBeNull();
            content.ValidationErrors.Should().Contain(e => e.PropertyName == nameof(PurchaseOrderCreate.SupplierId));


        }

        [Fact]
        public async Task ShouldCreatePurchaseOrderFail_EmptyPurchaseOrderItems_BadRequest()
        {
            PurchaseOrderCreate data = new PurchaseOrderCreate()
            {
                BranchId = Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4"),
                DeliveryDate = DateTime.Now.AddDays(1),
                SupplierId = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1"),
                PurchaseOrderItems = new List<PurchaseOrderItemCreate>() {}
            };

            var request = new CreatePurchaseOrderRequest()
            {
                Data = data
            };
            var response = await _client.PostAsJsonAsync("api/PurchaseOrder", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Branch Id is empty");

            var content = await response.Content.ReadAsAsync<ErrorResponse>();

            content.Should().NotBeNull();
            content.ValidationErrors.Should().Contain(e => e.PropertyName == nameof(PurchaseOrderCreate.PurchaseOrderItems));


        }

        [Fact]
        public async Task ShouldGetAtleastOnePurchaseOrderWithinTheWeekSuccess()
        {
            var endDate = DateTime.Today.AddDays(7);
            var response = await _client.GetAsync($"api/PurchaseOrder?endDate={endDate.ToString("yyyy-M-d")}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<PurchaseOrderView>>();

            content.Should().NotBeNull();
            content.Data.Should().HaveCountGreaterOrEqualTo(1);
            content.Data.Should().OnlyContain(po => po.DeliveryDate >= DateTime.Today && po.DeliveryDate <= endDate);

        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public async Task ShouldGetTenPurchaseOrder(int itemPerPage)
        {
            var response = await _client.GetAsync($"api/PurchaseOrder?itemPerPage={itemPerPage}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<PurchaseOrderView>>();

            content.Should().NotBeNull();
            content.Data.Should().HaveCount(itemPerPage);

        }

        [Fact]
        public async Task ShouldGetAtleastOnePurchaseOrderThatStartWith()
        {
            string startBarcode = "00";
            var response = await _client.GetAsync($"api/PurchaseOrder?barcode={startBarcode}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<GetManyResponse<PurchaseOrderView>>();

            content.Should().NotBeNull();
            content.Data.Should().HaveCountGreaterOrEqualTo(1);
            content.Data.Should().OnlyContain(po => po.Barcode.StartsWith(startBarcode));

        }


        [Fact]
        public async Task ShouldGetExactlyOnePurchaseOrderSuccess()
        {

            var id = Guid.Parse("afe14401-14c6-4e3d-a39a-fcfa6dabdab4");
            var response = await _client.GetAsync($"api/PurchaseOrder/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsAsync<PurchaseOrderView>();

            content.Should().NotBeNull();
            content.Id.Should().Be(id);
        }

        [Fact]
        public async Task ShouldGetExactlyOnePurchaseOrderFail_NotFound()
        {

            var id = Guid.Parse("afe14401-14c6-4e3d-a39a-fcfa6dabdab8");
            var response = await _client.GetAsync($"api/PurchaseOrder/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
