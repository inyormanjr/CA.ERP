using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.WebApp.Blazor.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IStockReceiveService
    {
        Task<Guid> GenerateStockReceiveFromPurchaseOrderAsync(PurchaseOrderView purchaseOrderView);
    }
    public class StockReceiveService : IStockReceiveService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string GenerateStockReceiveFromPurchaseOrderEndpoint = "/api/StockReceive/GenerateFromPurchaseOrder";

        public StockReceiveService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Guid> GenerateStockReceiveFromPurchaseOrderAsync(PurchaseOrderView purchaseOrderView)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, GenerateStockReceiveFromPurchaseOrderEndpoint);
            var response = await client.PostAsJsonAsync(uri, new StockReceiveGenerateFromPurchaseOrder(purchaseOrderView.Id));
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return (await response.Content.ReadFromJsonAsync<CreateResponse>()).Id;
        }
    }
}
