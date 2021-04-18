using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.ReportAgg;
using CA.ERP.Domain.StockAgg;
using FluentValidation.Results;
using jsreport.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Api
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

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Stock.StockView>>> Get(string brand = null, string model = null, string stockNumber = null, string serial = null, int pageSize = 10, int page = 1, CancellationToken cancellationToken = default)
        {
            var paginatedStocks = await _stockService.GetStocksAsync(brand, model, stockNumber, serial, pageSize, page, cancellationToken);
            var dtoStocks = _mapper.Map<List<Dto.Stock.StockView>>(paginatedStocks.Data);
            var response = new Dto.GetManyResponse<Dto.Stock.StockView>()
            {
                CurrentPage = paginatedStocks.CurrentPage,
                TotalPage = paginatedStocks.TotalPage,
                Data = dtoStocks,
                PageSize = paginatedStocks.PageSize,
                TotalCount = paginatedStocks.TotalCount,
            };
            return Ok(response);
        }

        /// <summary>
        /// Generate stock numbers
        /// </summary>
        /// <param name="branchId">The branch id to generate the stock number for</param>
        /// <param name="count">The number of stuck number to generate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GenerateStockNumbers/{branchId}/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<string>>> Get(Guid branchId, int count, CancellationToken cancellationToken)
        {
            var stockNumbers = await _stockService.GenerateStockNumbersAsync(branchId, count, cancellationToken);
            return Ok(new Dto.GetManyResponse<string>() { Data = stockNumbers.ToList() });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.Stock.StockUpdate> request, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> result = await _stockService.UpdateStockAsync(id, request.Data.MasterProductId, request.Data.SerialNumber, request.Data.CostPrice, cancellationToken);

            return result.Match<IActionResult>(
                f0: (guid) => NoContent(),
                f1: (validationErrors) => {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };
                    return BadRequest(response);
                },
                f2: (notFound) => NotFound()
            );
        }

    }
}
