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
    public class SupplierService
    {
        private const string GetSupplierEndpoint = "/api/Supplier";
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
    }
}
