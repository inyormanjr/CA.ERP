using AutoMapper;
using CA.ERP.Reporting.Repositories;
using CA.ERP.Reporting.Services;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using NetBarcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Reporting.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PurchaseOrderController : BaseReportController
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IMapper _mapper;
        private readonly IBarcodeService _barcodeService;

        public PurchaseOrderController(IServiceProvider serviceProvider, IPurchaseOrderRepository purchaseOrderRepository, IMapper mapper, IBarcodeService barcodeService)
            : base(serviceProvider)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
            _barcodeService = barcodeService;
        }
        


        [MiddlewareFilter(typeof(JsReportPipeline))]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(Guid id, Recipe format = Recipe.ChromePdf, CancellationToken cancellationToken = default)
        {
            HttpContext.JsReportFeature().Recipe(format);

            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id, cancellationToken);
            var reportDto = _mapper.Map<Dto.PurchaseOrder>(purchaseOrder);
            reportDto.Barcode = _barcodeService.GenerateBarcode(purchaseOrder.Barcode);
            return View(reportDto);
        }
    }
}
