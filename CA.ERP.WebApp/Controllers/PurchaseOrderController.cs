using AspNetCore.Reporting;
using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.ReportAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation.Results;
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

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseOrderController : BaseApiController
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IReportGenerator _reportGenerator;
        private readonly IBarcodeGenerator _barcodeGenerator;

        public PurchaseOrderController(IServiceProvider serviceProvider, IUserHelper userHelper, PurchaseOrderService purchaseOrderService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IReportGenerator reportGenerator, IBarcodeGenerator barcodeGenerator )
            :base(serviceProvider)
        {
            _purchaseOrderService = purchaseOrderService;
            _webHostEnvironment = webHostEnvironment;
            _reportGenerator = reportGenerator;
            _barcodeGenerator = barcodeGenerator;
            
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
        public async Task<ActionResult<Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderView>>> Get(CancellationToken cancellationToken)
        {
            var list = await _purchaseOrderService.GetManyAsync(cancellationToken);
            var dtoList = _mapper.Map<List<Dto.PurchaseOrder.PurchaseOrderView>>(list);
            var response = new Dto.GetManyResponse<Dto.PurchaseOrder.PurchaseOrderView>()
            {
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
        public async Task<IActionResult> Print(Guid id, RenderType renderType = RenderType.Pdf, CancellationToken cancellationToken = default)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return StatusCode(501);
            }
            var getOption = await _purchaseOrderService.GetOneAsync(id);

            return await Task.Run(() => {
                return getOption.Match<IActionResult>(
                f0: (purchaseOrder) =>
                {
                    //var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\PurchaseOrderReport.rdlc";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Reports", "PurchaseOrderReport.rdlc");

                    Dictionary<string, object> dataSources = new Dictionary<string, object>();
                    dataSources.Add("PurchaseOrderDataSet", new List<PurchaseOrder>() { purchaseOrder });
                    dataSources.Add("PurchaseOrderItemDataSet", purchaseOrder.PurchaseOrderItems);


                    byte[] barcodeImage = _barcodeGenerator.GenerateBarcode(purchaseOrder.Barcode);
                    dataSources.Add("BarcodeDataSet", new List<object>() { new { BarcodeImage = barcodeImage } });
                    

                    var result = _reportGenerator.GenerateReport(path, renderType, dataSources: dataSources);
                    return File(result.MainStream, _reportGenerator.GetMimeTypeFor(renderType));
                },
                f1: notFound => NotFound()
                );
            }, cancellationToken: cancellationToken);
        }


    }
}
