using CA.ERP.Domain.Core.DomainResullts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CA.ERP.WebApp.Controllers.Api
{
    public class ApiControllerBase : ControllerBase
    {

        protected ActionResult HandleDomainResult(DomainResult domainResult)
        {
            switch (domainResult.ErrorType)
            {

                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }
            ModelState.AddModelError(domainResult.ErrorCode, domainResult.ErrorMessage);

            return this.ValidationProblem();
        }
    }
}
