using CA.ERP.DataAccess;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.Stock;
using CA.ERP.WebApp.Dto.StockReceive;
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
using Microsoft.Extensions.DependencyInjection;
using CA.ERP.DataAccess.Entities;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class StockReceiveTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public StockReceiveTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreateStockReceiveSuccesfully()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                int i = 1;

                var dbContext = scope.ServiceProvider.GetService<CADataContext>();

                Guid purchaseOrderId = Guid.Parse("6b9e9264-f04a-4885-a649-dba5f0232227");

                var purchaseOrder = dbContext.PurchaseOrders.FirstOrDefault(po => po.Id == purchaseOrderId);

                StockReceiveCreate data = new StockReceiveCreate()
                {
                    BranchId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                    StockSource = Domain.StockReceiveAgg.StockSource.PurchaseOrder,
                    PurchaseOrderId = purchaseOrderId,
                    SupplierId = purchaseOrder.SupplierId
                };

                var purchaseOrderItems = dbContext.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == data.PurchaseOrderId).ToList();
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    data.Stocks.Add(new StockCreate()
                    {
                        CostPrice = purchaseOrderItem.CostPrice,
                        MasterProductId = purchaseOrderItem.MasterProductId,
                        PurchaseOrderItemId = purchaseOrderItem.Id,
                        SerialNumber = "XMN" + i.ToString("00000"),
                        StockNumber = "BBI" + i.ToString("00000"),
                        StockStatus = Domain.StockAgg.StockStatus.Available
                    });
                    i++;
                }
                var request = new CreateStockReceiveRequest()
                {
                    Data = data
                };
                var response = await _client.PostAsJsonAsync("api/StockReceive", request);

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content = await response.Content.ReadAsAsync<CreateResponse>();

                content.Should().NotBeNull();
                content.Id.Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public async Task ShouldCreateStockReceiveFail_WrongSupplier()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                int i = 1;

                var dbContext = scope.ServiceProvider.GetService<CADataContext>();

                Guid purchaseOrderId = Guid.Parse("6b9e9264-f04a-4885-a649-dba5f0232227");
                
                var purchaseOrder = dbContext.PurchaseOrders.FirstOrDefault(po => po.Id == purchaseOrderId);
                
                StockReceiveCreate data = new StockReceiveCreate()
                {
                    BranchId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                    StockSource = Domain.StockReceiveAgg.StockSource.PurchaseOrder,
                    PurchaseOrderId = purchaseOrderId,
                    SupplierId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                };


                var purchaseOrderItems = dbContext.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == data.PurchaseOrderId).ToList();
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    data.Stocks.Add(new StockCreate()
                    {
                        CostPrice = purchaseOrderItem.CostPrice,
                        MasterProductId = purchaseOrderItem.MasterProductId,
                        PurchaseOrderItemId = purchaseOrderItem.Id,
                        SerialNumber = "XMW" + i.ToString("00000"),
                        StockNumber = "BBG" + i.ToString("00000"),
                        StockStatus = Domain.StockAgg.StockStatus.Available
                    });
                    i++;
                }
                var request = new CreateStockReceiveRequest()
                {
                    Data = data
                };
                var response = await _client.PostAsJsonAsync("api/StockReceive", request);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

                var content = await response.Content.ReadAsAsync<ErrorResponse>();
                content.ValidationErrors.Should().OnlyContain( e => e.PropertyName == "SupplierId");

            }
        }


        [Fact]
        public async Task ShouldCreateStockReceiveFail_BadRequest_Duplicate_Serial_Or_Model()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                var dbContext = scope.ServiceProvider.GetService<CADataContext>();


                Guid purchaseOrderId = Guid.Parse("6b9e9264-f04a-4885-a649-dba5f0232227");
                var purchaseOrder = dbContext.PurchaseOrders.FirstOrDefault(po => po.Id == purchaseOrderId);
                StockReceiveCreate data = new StockReceiveCreate()
                {
                    BranchId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                    StockSource = Domain.StockReceiveAgg.StockSource.PurchaseOrder,
                    PurchaseOrderId = purchaseOrderId,
                    SupplierId = purchaseOrder.SupplierId,
                };

                
                var purchaseOrderItems = dbContext.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == data.PurchaseOrderId).ToList();
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    data.Stocks.Add(new StockCreate()
                    {
                        CostPrice = purchaseOrderItem.CostPrice,
                        MasterProductId = purchaseOrderItem.MasterProductId,
                        PurchaseOrderItemId = purchaseOrderItem.Id,
                        SerialNumber = "XMNLOIPO",
                        StockNumber = "B00000154",
                        StockStatus = Domain.StockAgg.StockStatus.Available
                    });
                }
                var request = new CreateStockReceiveRequest()
                {
                    Data = data
                };
                var response = await _client.PostAsJsonAsync("api/StockReceive", request);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                var content = await response.Content.ReadAsAsync<ErrorResponse>();
                content.ValidationErrors.Should().OnlyContain(e => e.PropertyName == "Stocks");
            }
        }



        [Fact]
        public async Task ShouldCreateStockReceiveFail_BadRequest_Empty_Serial()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                int i = 1;

                var dbContext = scope.ServiceProvider.GetService<CADataContext>();


                Guid purchaseOrderId = Guid.Parse("6b9e9264-f04a-4885-a649-dba5f0232227");
                var purchaseOrder = dbContext.PurchaseOrders.FirstOrDefault(po => po.Id == purchaseOrderId);
                StockReceiveCreate data = new StockReceiveCreate()
                {
                    BranchId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                    StockSource = Domain.StockReceiveAgg.StockSource.PurchaseOrder,
                    PurchaseOrderId = purchaseOrderId,
                    SupplierId = purchaseOrder.SupplierId,
                };

                var purchaseOrderItems = dbContext.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == data.PurchaseOrderId).ToList();
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    data.Stocks.Add(new StockCreate()
                    {
                        CostPrice = purchaseOrderItem.CostPrice,
                        MasterProductId = purchaseOrderItem.MasterProductId,
                        PurchaseOrderItemId = purchaseOrderItem.Id,
                        SerialNumber = "",
                        StockNumber = "" ,
                        StockStatus = Domain.StockAgg.StockStatus.Available
                    });
                    i++;
                }
                var request = new CreateStockReceiveRequest()
                {
                    Data = data
                };
                var response = await _client.PostAsJsonAsync("api/StockReceive", request);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                var content = await response.Content.ReadAsAsync<ErrorResponse>();
                content.ValidationErrors.Should().Contain(e => e.PropertyName == "Stocks");
            }
        }

        [Fact]
        public async Task ShouldCreateStockReceiveFail_BadRequest_Empty_StockNumber()
        {
            using (var scope = _factory.Services.CreateScope())
            {


                StockReceiveCreate data = new StockReceiveCreate()
                {
                    BranchId = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063"),
                    StockSource = Domain.StockReceiveAgg.StockSource.PurchaseOrder,
                    PurchaseOrderId = Guid.Parse("6b9e9264-f04a-4885-a649-dba5f0232227")
                };

                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                var purchaseOrderItems = dbContext.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == data.PurchaseOrderId).ToList();
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    data.Stocks.Add(new StockCreate()
                    {
                        CostPrice = purchaseOrderItem.CostPrice,
                        MasterProductId = purchaseOrderItem.MasterProductId,
                        PurchaseOrderItemId = purchaseOrderItem.Id,
                        SerialNumber = "",
                        StockNumber = "B00000154",
                        StockStatus = Domain.StockAgg.StockStatus.Available
                    });
                }
                var request = new CreateStockReceiveRequest()
                {
                    Data = data
                };
                var response = await _client.PostAsJsonAsync("api/StockReceive", request);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }
    }
}
