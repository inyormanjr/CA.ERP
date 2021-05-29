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
    public class ServiceBase<TCreate, TView> : ICreateService<TCreate>, IGetByIdService<TView> where TCreate : class where TView : class
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

        public async Task<TView> GetById(Guid id)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiErp);
            var uri = new Uri(client.BaseAddress, $"{_endPoint}/{id}");

            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<TView>();
        }
    }
}
