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

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : BaseApiController
    {
        private readonly BranchService _branchService;
        private readonly StockService _stockService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRenderService _renderService;

        public StockController(IServiceProvider serviceProvider, BranchService branchService , StockService stockService, IWebHostEnvironment webHostEnvironment, IRenderService renderService) : base(serviceProvider)
        {
            _branchService = branchService;
            _stockService = stockService;
            _webHostEnvironment = webHostEnvironment;
            _renderService = renderService;
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
            var stockNumbersOption = await _stockService.GenerateStockNumbersAsync(branchId, count);
            return stockNumbersOption.Match<ActionResult>(
                f0: stockNumbers => Ok(new Dto.GetManyResponse<string>() { Data = stockNumbers.ToList() }),
                f1: _ => NotFound(),
                f2: _ => Forbid()
                );
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


        [HttpGet("PrintStockList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Print([FromQuery]Guid branchId, [FromQuery]List<Guid> stockIds, CancellationToken cancellationToken = default)
        {


            var branchOption = await _branchService.GetOneAsync(branchId);
            return await branchOption.Match<Task<IActionResult>>(
                f0: async branch => {
                    List<Stock> stocks = await _stockService.GetManyAsync(branchId, stockIds.ToList());


                    var reportDto = new ReportDto.StockList();
                    reportDto.BranchContact = branch.Contact;
                    reportDto.BranchName = branch.Name;
                    reportDto.Date = DateTime.Now;
                    reportDto.Stocks = _mapper.Map<List<ReportDto.StockListItem>>(stocks);

                    var report = await _renderService.RenderByNameAsync("/stocks/stock-list/report", reportDto, cancellationToken);

                    return File(report.Content, report.Meta.ContentType);
                },
                f1: async _ => NotFound()
                );

            
        }
    }
}
