using AutoMapper;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CreateDirectStockReceive;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitStockReceiveFromPurchaseOrder;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromPurchaseOrder;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetManyStockReceive;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetOneStockReceive;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitDirectStockReceive;
using CA.ERP.Common.Types;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockReceiveController : ApiControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseOrderController> _logger;

        public StockReceiveController(IMediator mediator, IMapper mapper, ILogger<PurchaseOrderController> logger)
        {

            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("GenerateFromPurchaseOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> GenerateFromPurchaseOrder(Dto.StockReceive.StockReceiveGenerateFromPurchaseOrder request, CancellationToken cancellationToken)
        {

            var command = new GenerateStockReceiveFromPurchaseOrderCommand(request.PurchaseOrderId);

            var createResult = await _mediator.Send(command, cancellationToken);

            if (createResult.IsSuccess)
            {
                var dto = new Dto.CreateResponse() { Id = createResult.Result };
                return Ok(dto);
            }
            return HandleDomainResult(createResult);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dto.PaginatedResponse<Dto.StockReceive.StockReceiveView>>> GetMany(Guid? branchId, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, StockSource? source, StockReceiveStage? stage, int skip = 0, int take = 20, CancellationToken cancellationToken = default)
        {

            var query = new GetManyStockReceiveQuery(branchId, supplierId, dateCreated, dateReceived, source, stage, skip, take);

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.StockReceive.StockReceiveView>> GeOne(Guid id, CancellationToken cancellationToken = default)
        {

            var query = new GetOneStockReceiveQuery(id);

            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Result);
            }
            return HandleDomainResult(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CommitFromPurchaseOrder(Guid id, UpdateBaseRequest<StockReceiveCommit> request, CancellationToken cancellationToken = default)
        {
            var stockReceiveCommit = request.Data;

            var query = new CommitStockReceiveFromPurchaseOrderCommand(id, stockReceiveCommit);

            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return HandleDomainResult(result);
        }

        [HttpPut("{id}/commit-direct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CommitDirect(Guid id, CancellationToken cancellationToken = default)
        {

            var query = new CommitDirectStockReceiveCommand(id);

            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return HandleDomainResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Direct(StockReceiveCreate stockReceive, CancellationToken cancellationToken = default)
        {
            var query = new CreateDirectStockReceiveCommand(stockReceive);

            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(CreateResponse.Create(result.Result));
            }
            return HandleDomainResult(result);
        }
    }
}
