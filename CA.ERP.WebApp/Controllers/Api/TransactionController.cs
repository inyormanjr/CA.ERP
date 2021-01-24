using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CA.ERP.Domain.TransactionAgg;
using CA.ERP.Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA.ERP.WebApp.Controllers.Api
{
    public class TransactionController: BaseApiController
    {
        private readonly TransactionService _transactionService;

        public TransactionController(IServiceProvider serviceProvider, TransactionService transactionService)
            : base(serviceProvider)
        {
        _   _transactionService = transactionService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.Transaction.CreateTransactionRequest>> CreateSupplier(Dto.Transaction.CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            
            var transactionProducts = _mapper.Map<List<TransactionProduct>>(request.Data.TransactionProducts);
            var createResult = await _transactionService.CreateTransactionAsync(
              request.Data.BranchId,
              request.Data.TransactionType,
              request.Data.InterestType,
              request.Data.TransactionDate,
              request.Data.DeliveryDate,
              request.Data.TransactionNumber,
              request.Data.SalesmanId,
              request.Data.InvenstigatedById,
              request.Data.Total,
              request.Data.Balance,
              request.Data.UDI,
              request.Data.TotalRebate,
              request.Data.PN,
              request.Data.Terms,
              request.Data.GrossMonthly,
              request.Data.RebateMonthly,
              request.Data.NetMonthly,
              transactionProducts,
              cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (id) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = id,
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
            }
         );
        }
    }
}