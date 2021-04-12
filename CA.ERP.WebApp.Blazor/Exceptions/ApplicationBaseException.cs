using CA.ERP.WebApp.Blazor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Exceptions
{
    public class ApplicationBaseException : Exception
    {
        public ApplicationBaseException(string message) : base(message)
        {
        }

        public static async Task<ApplicationBaseException> Create(HttpResponseMessage httpResponseMessage)
        {
           
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorString = await httpResponseMessage.Content.ReadAsStringAsync();

                // Deserialize:
                var error =
                    JsonConvert.DeserializeObject<Error>(errorString);


                return new ValidationException(error.Title, error.Errors ?? new Dictionary<string, string[]>());

            }

            return new ApplicationBaseException("Unknow Error");
        }
    }
}
