using CA.ERP.WebApp.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Test.Integration.Helpers
{
    public static class Authentication
    {
        public static async Task AddAuthorizationHeader(this HttpClient httpClient)
        {
            var response = await httpClient.PostAsJsonAsync("api/Authentication/Login", new LoginRequest() { Username = "ExistingUser", Password = "password" });

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginResponse.token}");
            }
            else
            {
                Debugger.Log(0, "", "Login failed");
            }
        }
    }
}
