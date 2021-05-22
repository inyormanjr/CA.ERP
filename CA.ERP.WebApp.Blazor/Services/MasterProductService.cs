using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.MasterProduct;
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
    public interface IMasterProductService
    {
        Task<List<MasterProductView>> GetMasterProductsWithBrandAndSupplier(Guid brandId, Guid supplierId);
        Task<PaginatedResponse<MasterProductView>> GetMasterProducts(string model);
    }

    public class MasterProductService : IMasterProductService
    {
        private const string MASTER_PRODUCT_ENDPOINT = "/api/MasterProduct/";
        private readonly IHttpClientFactory _httpClientFactory;

        public MasterProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedResponse<MasterProductView>> GetMasterProducts(string model)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var uri = new Uri(client.BaseAddress, MASTER_PRODUCT_ENDPOINT);

            if (!string.IsNullOrEmpty(model))
            {
                uri = uri.AddQuery("model", model);
            }
            

            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<PaginatedResponse<MasterProductView>>();
        }

        public async Task<List<MasterProductView>> GetMasterProductsWithBrandAndSupplier(Guid brandId, Guid supplierId)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var uri = new Uri(client.BaseAddress, $"{MASTER_PRODUCT_ENDPOINT}/{brandId}/{supplierId}");


            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<List<MasterProductView>>();
        }
    }
}
