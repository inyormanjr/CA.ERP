using CA.ERP.WebApp.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("error")]
        public ActionResult<ErrorResponse> Error()
        {
            
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
            {
                GeneralError = $"Internal Server Error"
                
            };

            _logger.LogError(exception, "TraceId: {0}", HttpContext.TraceIdentifier);
            return StatusCode(500, response);
        }
    }
}
