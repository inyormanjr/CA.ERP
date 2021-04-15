using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Extensions;
using CA.ERP.WebApp.Blazor.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserService> _logger;
        private const string UserEndpoint = "/api/User";

        public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<PaginatedResponse<UserView>> GetUsersAsync(string firstName, string lastName, int page, int size)
        {
            var client = _httpClientFactory.CreateClient(Constants.ApiIdentity);
            Pagination pagination = new Pagination(page, size);

            _logger.LogDebug("pagination", pagination);

            var uri = new Uri(client.BaseAddress, UserEndpoint);

            uri = uri.AddQuery("skip", pagination.Skip.ToString());
            uri = uri.AddQuery("take", pagination.Take.ToString());
            if (!string.IsNullOrEmpty(firstName))
            {
                uri = uri.AddQuery("firstName", firstName);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                uri = uri.AddQuery("lastName", lastName);
            }


            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {

                throw await ApplicationBaseException.Create(response);
            }
            return await response.Content.ReadFromJsonAsync<PaginatedResponse<UserView>>();
        }
    }
}
