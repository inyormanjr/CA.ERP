using CA.ERP.Application.CommandQuery.StockTransferCommandQuery.CreateStockTransfer;
using CA.ERP.Application.CommandQuery.StockTransferCommandQuery.GetManyStockTransfer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransferController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public StockTransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.CreateBaseRequest<Dto.StockTransfer.StockTransferCreate> request, CancellationToken cancellationToken)
        {

            var command = new CreateStockTransferCommand(request.Data);

            var createResult = await _mediator.Send(command, cancellationToken);
            if (createResult.IsSuccess)
            {
                var response = new Dto.CreateResponse()
                {
                    Id = createResult.Result
                };

                return Ok(response);
            }
            return HandleDomainResult(createResult);

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PaginatedResponse<Dto.StockTransfer.StockTransferView>>> Get([FromQuery]GetManyStockTransferQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }
}
}
