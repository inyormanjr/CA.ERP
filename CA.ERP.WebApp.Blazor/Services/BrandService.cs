using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Brand;
using CA.ERP.WebApp.Blazor.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IBrandService
    {
        Task<PaginatedResponse<BrandView>> GetBrandsAsync();
    }

    public class BrandService : IBrandService
    {
        private const string BRAND_ENDPOINT = "/api/Brand";
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedResponse<BrandView>> GetBrandsAsync()
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var uri = new Uri(client.BaseAddress, BRAND_ENDPOINT);



            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            var paginatedSuppliers = await response.Content.ReadFromJsonAsync<PaginatedResponse<BrandView>>();

            return paginatedSuppliers;
        }
    }
}
