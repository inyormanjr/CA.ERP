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
            var httpErrorObject = await httpResponseMessage.Content.ReadAsStringAsync();

            var anonymousErrorObject =  new { Message = "", ModelState = new Dictionary<string, string[]>() };

            // Deserialize:
            var deserializedErrorObject =
                JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {

                return new ValidationException(anonymousErrorObject.Message, deserializedErrorObject.ModelState ?? new Dictionary<string, string[]>());

            }

            return new ApplicationBaseException("Unknow Error");
        }
    }
}
