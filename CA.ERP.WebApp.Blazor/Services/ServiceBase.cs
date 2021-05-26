using CA.ERP.Shared.Dto;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Inferfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public abstract class ServiceBase<TCreate> : ICreateService<TCreate> where TCreate : class
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        protected readonly string _endPoint;

        public ServiceBase(IHttpClientFactory httpClientFactory, string endPoint)
        {
            _httpClientFactory = httpClientFactory;
            _endPoint = endPoint;
        }

        public async Task<Guid> CreateAsync(TCreate dto)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);



            var uri = new Uri(client.BaseAddress, _endPoint);

            var createRequest = new CreateBaseRequest<TCreate>() { Data = dto };

            var response = await client.PostAsJsonAsync(uri, createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await ApplicationBaseException.Create(response);
                throw exception;
            }

            var createResponse = await response.Content.ReadFromJsonAsync<CreateResponse>();
            return createResponse.Id;
        }
    }
}
