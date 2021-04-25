using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Models;
using CA.ERP.WebApp.Blazor.Options;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public interface IPurchaseOrderService
    {
        Task<Guid> CreatePurchaseOrderAsync(PurchaseOrderCreate purchaseOrder);
        string GetPurchaseOrderReportUrl(Guid purchaseOrderId);
        Task<PaginatedResponse<PurchaseOrderView>> GetPurchaseOrdersAsync(Guid? branchId, string purchaseOrderNumber, DateTimeOffset? startDate, DateTimeOffset? endDate, PurchaseOrderStatus? purchaseOrderStatus, int page, int size);
    }
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private const string PurchaseOrderEndpoint = "/api/PurchaseOrder";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PurchaseOrderService> _logger;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly BaseAddresses _baseAddresses;
        private  string _accessToken = string.Empty;

        public PurchaseOrderService(IHttpClientFactory httpClientFactory, ILogger<PurchaseOrderService> logger, IOptions<BaseAddresses> baseAddressesOptions, IAccessTokenProvider accessTokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _accessTokenProvider = accessTokenProvider;
            _baseAddresses = baseAddressesOptions.Value;
            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            var accessTokenResult = await _accessTokenProvider.RequestAccessToken();
            if (accessTokenResult.TryGetToken(out AccessToken accessToken))
            {
                _accessToken = accessToken.Value;
            }
        }
        public async Task<PaginatedResponse<PurchaseOrderView>> GetPurchaseOrdersAsync(Guid? branchId,string purchaseOrderNumber, DateTimeOffset? startDate, DateTimeOffset? endDate, PurchaseOrderStatus? purchaseOrderStatus, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            Pagination pagination = new Pagination(page, size);

            _logger.LogDebug("pagination", pagination);

            var uri = new Uri(client.BaseAddress, PurchaseOrderEndpoint);

            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());
            if (branchId != null)
            {
                uri = uri.AddQuery("branchId", branchId.Value.ToString());

            }
            if (purchaseOrderStatus != null)
            {
                uri = uri.AddQuery("purchaseOrderStatus", purchaseOrderStatus.Value.ToString());

            }
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
            {
                uri = uri.AddQuery("barcode", purchaseOrderNumber);
            }
            if (startDate != null)
            {
                uri = uri.AddQuery("startDate", startDate.Value.DateTime.ToString("o"));
                _logger.LogDebug($"startDate : {startDate.Value.DateTime.ToString("o")}");
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
            return await response.Content.ReadFromJsonAsync<PaginatedResponse<PurchaseOrderView>>();
        }

        public async Task<Guid> CreatePurchaseOrderAsync(PurchaseOrderCreate purchaseOrder)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);



            var uri = new Uri(client.BaseAddress, PurchaseOrderEndpoint);

            var createRequest = new CreateBaseRequest<PurchaseOrderCreate>() { Data = purchaseOrder };

            var response = await client.PostAsJsonAsync(uri, createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await ApplicationBaseException.Create(response);
                throw exception;
            }

            var createResponse = await response.Content.ReadFromJsonAsync<CreateResponse>();
            return createResponse.Id;
        }

        public  string GetPurchaseOrderReportUrl(Guid purchaseOrderId)
        {
            var baseAddress = new Uri(_baseAddresses.Report);
            var uri = new Uri(baseAddress, $"/purchaseorder/{purchaseOrderId}");


            uri = uri.AddQuery("access_token", _accessToken);

            return uri.ToString();
        }
    }
}
