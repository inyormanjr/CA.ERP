using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockTransfer;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Inferfaces.Services;
using CA.ERP.WebApp.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IStockTransferService : ICreateService<StockTransferCreate>, IGetByIdService<StockTransferView>
    {
        Task<PaginatedResponse<StockTransferView>> GetStockTransfersAsync(string number = null, StockTransferStatus? stockTransferStatus = null, int page = 0, int size = 10);
    }

    public class StockTransferService : ServiceBase<StockTransferCreate, StockTransferView>, IStockTransferService
    {
        public StockTransferService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "api/StockTransfer")
        {
        }


        public async Task<PaginatedResponse<StockTransferView>> GetStockTransfersAsync(string number, StockTransferStatus? stockTransferStatus, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, _endPoint);

            Pagination pagination = new Pagination(page, size);


            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());

            if (!string.IsNullOrEmpty(number))
            {
                uri = uri.AddQuery("number", number);
            }
            if (stockTransferStatus != null)
            {
                uri = uri.AddQuery("stockTransferStatus", ((int)stockTransferStatus.Value).ToString());
            }
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<PaginatedResponse<StockTransferView>>();
        }
    }
}