using CA.ERP.Application.CommandQuery.StockCommandCommandQuery.GetManyStock;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.StockAgg;
using FluentValidation.Results;
using jsreport.Shared;
using MediatR;
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
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get Paginated Stocks
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PaginatedResponse<Dto.Stock.StockView>>> Get([FromQuery]GetManyStockQuery query, CancellationToken cancellationToken = default)
        {
            var paginatedStocks = await _mediator.Send(query);

            return Ok(paginatedStocks);
        }


       

    }
}
