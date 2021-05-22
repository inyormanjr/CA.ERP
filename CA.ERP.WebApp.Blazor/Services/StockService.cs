using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IStockService
    {
        Task<PaginatedResponse<StockView>> GetStocks(Guid? branchId, Guid? brandId, Guid? masterProductId, string stockNumber, string serialNumber, StockStatus? stockStatus, int page, int size);
    }

    public class StockService : IStockService
    {
        private readonly ILogger<StockService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string StockEndpoint = "/api/Stock";

        public StockService(ILogger<StockService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedResponse<StockView>> GetStocks(Guid? branchId, Guid? brandId, Guid? masterProductId, string stockNumber, string serialNumber, StockStatus? stockStatus, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            Pagination pagination = new Pagination(page, size);

            _logger.LogDebug("pagination", pagination);

            var uri = new Uri(client.BaseAddress, StockEndpoint);

            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());

            if (brandId != null)
            {
                uri = uri.AddQuery("brandId", brandId.Value.ToString());

            }
            if (branchId != null)
            {
                uri = uri.AddQuery("branchId", branchId.Value.ToString());

            }
            if (masterProductId != null)
            {
                uri = uri.AddQuery("masterProductId", masterProductId.Value.ToString());

            }
            if (!string.IsNullOrEmpty(stockNumber))
            {
                uri = uri.AddQuery("stockNumber", stockNumber);
            }
            if (!string.IsNullOrEmpty(serialNumber))
            {
                uri = uri.AddQuery("serialNumber", serialNumber);

            }

            if (stockStatus != null)
            {
                uri = uri.AddQuery("stockStatus", ((int)stockStatus).ToString());
            }

            _logger.LogInformation("uri : {uri}", uri.ToString());

            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }

            var ret =  await response.Content.ReadFromJsonAsync<PaginatedResponse<StockView>>();
            return ret;
        }
    }
}
