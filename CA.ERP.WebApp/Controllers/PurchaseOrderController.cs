using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : BaseApiController
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly IUserHelper _userHelper;
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IMapper _mapper;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, IUserHelper userHelper, PurchaseOrderService purchaseOrderService, IMapper mapper )
        {
            _logger = logger;
            _userHelper = userHelper;
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.PurchaseOrder.CreatePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating purchase order.", _userHelper.GetCurrentUserId());
            var purchaseOrderItems = _mapper.Map<List<PurchaseOrderItem>>(request.Data.PurchaseOrderItems);
            var createResult = await _purchaseOrderService.CreatePurchaseOrder(request.Data.DeliveryDate, request.Data.SupplierId, request.Data.BranchId, purchaseOrderItems, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (purchaseOrderId) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = purchaseOrderId
                };
                _logger.LogInformation("User {0} creating purchase order succeeded.", _userHelper.GetCurrentUserId());
                return Ok(response);
            },
            f1: (validationErrors) =>
            {
                var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                _logger.LogInformation("User {0} supplier branch creation failed.", _userHelper.GetCurrentUserId());
                return BadRequest(response);
            }
         );
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
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.PurchaseOrder.PurchaseOrderCreate> request, CancellationToken cancellationToken)
        {
            var domData = _mapper.Map<PurchaseOrder>(request.Data);
            OneOf<Guid, List<ValidationFailure>, NotFound> result = await _purchaseOrderService.UpdateAsync(id, domData, cancellationToken);

            return result.Match<IActionResult>(
                f0: (guid) => NoContent(),
                f1: (validationErrors) => {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} purhcase order update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (notFound) => NotFound()
            );
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderView>>> Get(CancellationToken cancellationToken)
        {
            var list = await _purchaseOrderService.GetManyAsync(cancellationToken);
            var dtoList = _mapper.Map<List<Dto.PurchaseOrder.PurchaseOrderItemView>>(list);
            var response = new Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderItemView>()
            {
                Data = dtoList
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PurchaseOrder.PurchaseOrderItemView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var option = await _purchaseOrderService.GetOneAsync(id, cancellationToken);

            return option.Match<ActionResult>(
                f0: data => Ok(_mapper.Map<Dto.PurchaseOrder.PurchaseOrderItemView>(data)),
                f1: notfound => NotFound()
                );
        }

        /// <summary>
        /// Delete purchase order by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var option = await _purchaseOrderService.DeleteAsync(id, cancellationToken);
            return option.Match<ActionResult>(
                f0: success =>
                {
                    return NoContent();
                },
                f1: notfound => NotFound()
            );
        }
    }
}
