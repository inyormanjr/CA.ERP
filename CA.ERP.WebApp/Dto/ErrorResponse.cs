using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class ErrorResponse
    {
        public ErrorResponse(string traceId)
        {
            TraceId = traceId;
        }
        public string TraceId { get; set; }
        public string GeneralError { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
