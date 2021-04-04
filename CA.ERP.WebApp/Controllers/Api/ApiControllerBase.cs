using CA.ERP.Domain.Core.DomainResullts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Api
{
    public class ApiControllerBase : ControllerBase
    {
        public ActionResult HandleDomainResult(DomainResult domainResult)
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
            return BadRequest(domainResult);
        }
    }
}
