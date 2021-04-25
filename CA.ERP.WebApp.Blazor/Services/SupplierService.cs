using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Supplier;
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
    public interface ISupplierService
    {
        Task<List<SupplierBrandView>> GetSupplierBrandsAsync(Guid supplierId);
        Task<PaginatedResponse<SupplierView>> GetSuppliersAsync(string name, int skip, int take);
    }
    public class SupplierService : ISupplierService
    {
        private const string GetSupplierEndpoint = "/api/Supplier";
        private const string GetSupplierBrandsEndpoint = "/api/Supplier/{0}/Brand";
        private readonly IHttpClientFactory _httpClientFactory;

        public SupplierService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PaginatedResponse<SupplierView>> GetSuppliersAsync(string name, int skip, int take)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var uri = new Uri(client.BaseAddress, GetSupplierEndpoint);

            uri = uri.AddQuery("skip", skip.ToString());
            uri = uri.AddQuery("take", take.ToString());

            if (!string.IsNullOrEmpty(name))
            {
                uri = uri.AddQuery("name", name);
            }


            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            var paginatedSuppliers = await response.Content.ReadFromJsonAsync<PaginatedResponse<SupplierView>>();

            return paginatedSuppliers;
        }

        public async Task<List<SupplierBrandView>> GetSupplierBrandsAsync(Guid supplierId)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);

            var uri = new Uri(client.BaseAddress, string.Format(GetSupplierBrandsEndpoint, supplierId));

            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<List<SupplierBrandView>>();

        }
    }
}
