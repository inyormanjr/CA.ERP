using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.UserAgg;
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
        public async Task<ActionResult<Dto.CreateResponse>> CreateSupplier(Dto.CreatePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating purchase order.", _userHelper.GetCurrentUserId());
            var purchaseOrderItems = _mapper.Map<List<PurchaseOrderItem>>(request.PurchaseOrderItems);
            var createResult = await _purchaseOrderService.CreatePurchaseOrder(request.DeliveryDate, request.SupplierId, request.BranchId, purchaseOrderItems, cancellationToken: cancellationToken);
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
    }
}
