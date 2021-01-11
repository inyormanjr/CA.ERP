using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class StockReceiveController : BaseApiController
    {
        private readonly StockReceiveService _stockReceiveService;

        public StockReceiveController(IServiceProvider serviceProvider, StockReceiveService stockReceiveService)
            :base(serviceProvider)
        {
            _stockReceiveService = stockReceiveService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.StockReceive.CreateStockReceiveRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating stock receive.", _userHelper.GetCurrentUserId());
            var stocks = _mapper.Map<List<Stock>>(request.Data.Stocks);
            var createResult = await _stockReceiveService.CreateStockReceive(request.Data.PurchaseOrderId, request.Data.BranchId, request.Data.StockSource, request.Data.SupplierId, stocks, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (purchaseOrderId) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = purchaseOrderId
                };
                _logger.LogInformation("User {0} creating stock receive succeeded.", _userHelper.GetCurrentUserId());
                return Ok(response);
            },
            f1: (validationErrors) =>
            {
                var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                _logger.LogInformation("User {0} stock receive creation failed.", _userHelper.GetCurrentUserId());
                return BadRequest(response);
            },
            f2: _ => Forbid()
         );
        }
    }
}
