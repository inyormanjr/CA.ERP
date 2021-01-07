using CA.ERP.Domain.StockAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : BaseApiController
    {
        private readonly StockService _stockService;

        public StockController(IServiceProvider serviceProvider, StockService stockService) : base(serviceProvider)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Generate stock numbers
        /// </summary>
        /// <param name="branchId">The branch id to generate the stock number for</param>
        /// <param name="count">The number of stuck number to generate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GenerateStockNumbers/{prefix}/{starting}/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<string>>> Get(string prefix, string starting, int count, CancellationToken cancellationToken)
        {
            var stockNumbers = await _stockService.GenerateStockNumbersAsync(prefix, starting, count);
            return Ok(stockNumbers);
        }
    }
}
