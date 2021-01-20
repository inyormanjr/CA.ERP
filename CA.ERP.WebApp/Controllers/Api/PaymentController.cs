using CA.ERP.Domain.PaymentAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        private readonly PaymentService _paymentService;

        public PaymentController(IServiceProvider serviceProvider, PaymentService paymentService) : base(serviceProvider)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.Payment.CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var createResult = await _paymentService.Create(request.Data.BranchId, request.Data.OfficialReceiptNumber, request.Data.PaymentType, request.Data.PaymentMethod, request.Data.PaymentDate, request.Data.GrossAmount, request.Data.Rebate, request.Data.Interest, request.Data.Discount, request.Data.Remarks, request.Data.TenderAmount, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (paymentId) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = paymentId
                };
                return Ok(response);
            },
            f1: (validationErrors) =>
            {
                var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                return BadRequest(response);
            },
            f2: _ => Forbid()
         );
        }
    }
}
