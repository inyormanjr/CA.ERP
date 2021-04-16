using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using jsreport.AspNetCore;
using jsreport.Types;

namespace CA.ERP.Reporting.Controllers
{
    [Route("[controller]")]
    public class BaseReportController : Controller
    {
        private IConfiguration _configuration;

        public BaseReportController(IServiceProvider serviceProvider)
        {
            _configuration = serviceProvider.GetService<IConfiguration>();
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string webAppUrl = _configuration.GetSection("WebAppUrl")?.Value ?? "http://webapp/";
            ViewData.Add("WebAppUrl", webAppUrl);
            ViewData.Add("DateFormat", "MM/dd/yyyy");
            ViewData.Add("MoneyFormat", "C");
            ViewData.Add("QuantityFormat", "0.###");
        }

    }
}
