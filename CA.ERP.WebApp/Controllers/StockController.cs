using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : BaseApiController
    {
        public StockController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
