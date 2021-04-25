using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Models;
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
        StockReceiveCommit ConvertStockReceiveViewToCommit(StockReceiveView stockReceiveView);
        Task<Guid> GenerateStockReceiveFromPurchaseOrderAsync(PurchaseOrderView purchaseOrderView);
        Task<StockReceiveView> GetStockReceiveByIdWithItems(Guid id);
        Task<PaginatedResponse<StockReceiveView>> GetStockReceivesAsync(Guid? branchId, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, int page, int size);
        Task Commit(StockReceiveCommit stockReceive);
    }
    public class StockReceiveService : IStockReceiveService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string GenerateStockReceiveFromPurchaseOrderEndpoint = "/api/StockReceive/GenerateFromPurchaseOrder";
        private const string StockReceiveEndpoint = "/api/StockReceive";


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

        public async Task<PaginatedResponse<StockReceiveView>> GetStockReceivesAsync(Guid? branchId, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, StockReceiveEndpoint);

            Pagination pagination = new Pagination(page, size);


            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());
            if (branchId != null)
            {
                uri = uri.AddQuery("branchId", branchId.ToString());
            }

            if (supplierId != null)
            {
                uri = uri.AddQuery("supplierId", supplierId.ToString());
            }

            if (dateCreated != null)
            {
                uri = uri.AddQuery("dateCreated", dateCreated.Value.DateTime.ToString("o"));
            }

            if (dateReceived != null)
            {
                uri = uri.AddQuery("dateReceived", dateReceived.Value.DateTime.ToString("o"));
            }

            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<PaginatedResponse<StockReceiveView>>();
        }

        public async Task<StockReceiveView> GetStockReceiveByIdWithItems(Guid id)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, StockReceiveEndpoint + "/" + id.ToString());
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<StockReceiveView>();
        }

        public StockReceiveCommit ConvertStockReceiveViewToCommit(StockReceiveView stockReceiveView)
        {

           StockReceiveCommit commit = new StockReceiveCommit() {
               Id = stockReceiveView.Id,
                BranchName = stockReceiveView.BranchName,
                SupplierName = stockReceiveView.SupplierName,
                DateCreated = stockReceiveView.DateCreated,
                DateReceived = stockReceiveView.DateReceived,
                Stage = stockReceiveView.Stage,
           };
            foreach (var item in stockReceiveView.Items)
            {
                StockReceiveItemCommit itemCommit = new StockReceiveItemCommit() {
                    Id = item.Id,
                    BrandName = item.BrandName,
                    Model = item.Model,
                    PurchaseOrderItemId = item.PurchaseOrderItemId,
                    CostPrice = item.CostPrice,
                    Status = item.Status,
                    StockNumber = item.StockNumber,
                    SerialNumber = item.SerialNumber
                };
                commit.Items.Add(itemCommit);
            }

            return commit;
        }

        public async Task Commit(StockReceiveCommit stockReceive)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, StockReceiveEndpoint + "/" + stockReceive.Id.ToString());
            var response = await client.PutAsJsonAsync(uri, stockReceive);
            if (!response.IsSuccessStatusCode)
            {
                throw await ApplicationBaseException.Create(response);
            }

        }
    }
}
