using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.Stock;
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
    public class StockTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public StockTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldUpdateStockSuccess_NoContent()
        {
            var random = new Random();
            var stockId = Guid.Parse("75068c88-af89-4f78-90ad-0212b6fc379d");
            UpdateBaseRequest<StockUpdate> request = new UpdateBaseRequest<StockUpdate>() { Data = new StockUpdate() { CostPrice = random.Next(100, 1000), MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), SerialNumber = "XMPSERIA001L" } };
            var response = await _client.PutAsJsonAsync($"api/Stock/{stockId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateStockFail_NotFound()
        {
            var random = new Random();
            var stockId = Guid.Parse("75068c88-af89-4f78-90ad-0212b6fc3999");
            UpdateBaseRequest<StockUpdate> request = new UpdateBaseRequest<StockUpdate>() { Data = new StockUpdate() { CostPrice = random.Next(100, 1000), MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), SerialNumber = "XMPSERIA001L" } };
            var response = await _client.PutAsJsonAsync($"api/Stock/{stockId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldUpdateStockFail_BadRequest()
        {
            var random = new Random();
            var stockId = Guid.Parse("75068c88-af89-4f78-90ad-0212b6fc379d");
            UpdateBaseRequest<StockUpdate> request = new UpdateBaseRequest<StockUpdate>() { Data = new StockUpdate() { CostPrice = random.Next(100, 1000), MasterProductId = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef"), SerialNumber = "000000004" } };
            var response = await _client.PutAsJsonAsync($"api/Stock/{stockId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Duplicate serial number");
        }
    }
}
