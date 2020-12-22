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
            Guid errorId = Guid.NewGuid();
            var response = new Dto.ErrorResponse()
            {
                GeneralError = $"Internal Server Error with Error Id: {errorId}"
            };

            _logger.LogError(exception, "ErrorId: {0}", errorId);
            return StatusCode(500, response);
        }
    }
}
