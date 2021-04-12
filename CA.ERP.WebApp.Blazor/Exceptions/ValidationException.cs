using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Exceptions
{
    public class ValidationException : ApplicationBaseException
    {
        public IDictionary<string, string[]> ValidationErrors { get; set; }
        public ValidationException(string message, IDictionary<string, string[]> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }


    }
}
