using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.ReportAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation.Results;
using jsreport.AspNetCore;
using jsreport.Shared;
using jsreport.Types;
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

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseOrderController : BaseApiController
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IBarcodeGenerator _barcodeGenerator;
        private readonly IReportGenerator _reportGenerator;

        public PurchaseOrderController(IServiceProvider serviceProvider, IUserHelper userHelper, PurchaseOrderService purchaseOrderService,  IBarcodeGenerator barcodeGenerator, IReportGenerator reportGenerator )
            :base(serviceProvider)
        {
            _purchaseOrderService = purchaseOrderService;
            _barcodeGenerator = barcodeGenerator;
            _reportGenerator = reportGenerator;
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
            },
            f2: _ => Forbid()
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
        [Obsolete]
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.PurchaseOrder.PurchaseOrderUpdate> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public async Task<ActionResult<Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderView>>> Get(string barcode = null,DateTime? startDate = null, DateTime? endDate = null, int pageSize = 10, int page = 1, CancellationToken cancellationToken = default)
        {


            var paginatedPurchaseOrders = await _purchaseOrderService.GetManyAsync(barcode, startDate, endDate, pageSize, page, cancellationToken);
            var dtoList = _mapper.Map<List<Dto.PurchaseOrder.PurchaseOrderView>>(paginatedPurchaseOrders.Data);
            var response = new Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderView>()
            {
                CurrentPage = paginatedPurchaseOrders.CurrentPage,
                TotalPage = paginatedPurchaseOrders.TotalPage,
                PageSize = paginatedPurchaseOrders.PageSize,
                TotalCount = paginatedPurchaseOrders.TotalCount,
                Data = dtoList
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PurchaseOrder.PurchaseOrderView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var option = await _purchaseOrderService.GetOneAsync(id, cancellationToken);

            return option.Match<ActionResult>(
                f0: data => Ok(_mapper.Map<Dto.PurchaseOrder.PurchaseOrderView>(data)),
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

        /// <summary>
        /// Print the given Purchase order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="renderType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}/Print")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Print(Guid id, CancellationToken cancellationToken = default)
        {

            var getOption = await _purchaseOrderService.GetOneAsync(id);

            return await getOption.Match<Task<IActionResult>>(
                f0: async (purchaseOrder) =>
                {
                    var reportDto = _mapper.Map<ReportDto.PurchaseOrder>(purchaseOrder);
                    reportDto.Barcode = _barcodeGenerator.GenerateBarcode(purchaseOrder.Barcode);
                    
                    string reportName = "PurchaseOrder";
                    var reportResult = await _reportGenerator.GenerateReport(reportName, reportDto);
                    return reportResult.Match<IActionResult>(
                        f0: report =>  File(report.Content, report.ContentType),
                        f1: _ => NotFound()
                    );
                },
                f1: async notFound => NotFound()
            );
        }


    }
}
