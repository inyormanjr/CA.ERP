using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public class PurchaseOrderService
    {
        private const string GetPurchaseOrderEndpoint = "/api/PurchaseOrder";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PurchaseOrderService> _logger;

        public PurchaseOrderService(IHttpClientFactory httpClientFactory, ILogger<PurchaseOrderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<PaginatedResponse<PurchaseOrderView>> GetPurchaseOrdersAsync(string purchaseOrderNumber, DateTimeOffset? startDate, DateTimeOffset? endDate, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            Pagination pagination = new Pagination(page, size);

            _logger.LogInformation("pagination", pagination);

            var uri = new Uri(client.BaseAddress, GetPurchaseOrderEndpoint);

            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
            {
                uri = uri.AddQuery("barcode", purchaseOrderNumber);
            }
            if (startDate != null)
            {
                uri = uri.AddQuery("startDate", startDate.Value.DateTime.ToString("o"));
                _logger.LogInformation($"startDate : {startDate.Value.DateTime.ToString("o")}");
            }

            if (endDate != null)
            {
                uri = uri.AddQuery("endDate", endDate.Value.DateTime.ToString("o"));
            }


            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                
                throw await ApplicationBaseException.Create(response);
            }
            var paginatedPurchaseOrders = await response.Content.ReadFromJsonAsync<PaginatedResponse<PurchaseOrderView>>();
            return paginatedPurchaseOrders;
        }
    }
}