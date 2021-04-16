using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IBranchService
    {
        Task<PaginatedResponse<BranchView>> GetBranchesAsync();
    }
    public class BranchService : IBranchService
    {
        private const string GetBranchEndpoint = "/api/Branch";
        private readonly IHttpClientFactory _httpClientFactory;

        public BranchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PaginatedResponse<BranchView>> GetBranchesAsync()
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var response = await client.GetAsync(GetBranchEndpoint);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            var paginatedPurchaseOrders = await response.Content.ReadFromJsonAsync<PaginatedResponse<BranchView>>();

            return paginatedPurchaseOrders;
        }
    }
}
