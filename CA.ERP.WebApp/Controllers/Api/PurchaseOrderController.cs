using AutoMapper;
using CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.CreatePurchaseOrder;
using CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetManyPurchaseOrder;
using CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetOnePurchaseOrder;
using CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.UpdatePurchaseOrder;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation.Results;
using jsreport.AspNetCore;
using jsreport.Shared;
using jsreport.Types;
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
using System.Drawing;
using System.Drawing.Imaging;
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
    public class PurchaseOrderController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IMediator mediator, IMapper mapper, ILogger<PurchaseOrderController> logger)
        {

            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create master products
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.PurchaseOrder.CreatePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var createPurchaseOrderItems = request.Data.PurchaseOrderItems.Select(poi => new CreatePurchaseOrderItem(poi.MasterProductId, poi.OrderedQuantity, poi.FreeQuantity, poi.CostPrice, poi.Discount, poi.TotalCostPrice, poi.DeliveredQuantity));
            var command = new CreatePurchaseOrderCommand(request.Data.DeliveryDate, request.Data.SupplierId, request.Data.DestinationBranchId, createPurchaseOrderItems.ToList());

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

        /// <summary>
        /// Update purchase order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [Obsolete]
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.PurchaseOrder.PurchaseOrderUpdate> request, CancellationToken cancellationToken)
        {
            var updatePurchaseOrderItems = request.Data.PurchaseOrderItems.Select(poi => new UpdatePurchaseOrderItem(poi.Id, id, poi.MasterProductId, poi.OrderedQuantity, poi.FreeQuantity, poi.CostPrice, poi.Discount, poi.DeliveredQuantity));
            var command = new UpdatePurchaseOrderCommand(id,request.Data.DeliveryDate, request.Data.SupplierId, request.Data.DestinationBranchId, updatePurchaseOrderItems.ToList());

            var createResult = await _mediator.Send(command, cancellationToken);
            if (createResult.IsSuccess)
            {
                return NoContent();
            }
            return HandleDomainResult(createResult);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PaginatedResponse<Dto.PurchaseOrder.PurchaseOrderView>>> Get(string barcode = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, int skip = 0, int take = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetManyPurchaseOrderQuery(skip, take, barcode:barcode, startDate:startDate, endDate:endDate);

            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                var dtoList = _mapper.Map<List<Dto.PurchaseOrder.PurchaseOrderView>>(result.Result.Data.ToList());
                var response = new Dto.PaginatedResponse<Dto.PurchaseOrder.PurchaseOrderView>()
                {
                    TotalCount = result.Result.TotalCount,
                    Data = dtoList
                };
                return Ok(response);
            }
            return HandleDomainResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PurchaseOrder.PurchaseOrderView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOnePurchaseOrderQuery(id);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Result);
            }
            return HandleDomainResult(result);
        }




    }
}
