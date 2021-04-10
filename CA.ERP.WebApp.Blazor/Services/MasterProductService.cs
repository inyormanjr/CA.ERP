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
    public class MasterProductService
    {
        private const string GetBranchEndpoint = "/api/MasterProduct/{0}/{1}";
        private readonly IHttpClientFactory _httpClientFactory;

        public MasterProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<MasterProductView>> GetMasterProductsWithBrandAndSupplier(Guid brandId, Guid supplierId)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);



            var uri = new Uri(client.BaseAddress, string.Format( GetBranchEndpoint, brandId.ToString(), supplierId.ToString()));


            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<List<MasterProductView>>();
        }
    }
}
