using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.ReportAgg;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Report
{
    [Authorize]
    public class PurchaseOrderController : BaseReportController
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IMapper _mapper;
        private readonly IBarcodeGenerator _barcodeGenerator;

        public PurchaseOrderController(IServiceProvider serviceProvider, PurchaseOrderService purchaseOrderService, IMapper mapper, IBarcodeGenerator barcodeGenerator)
            : base(serviceProvider)
        {
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
            _barcodeGenerator = barcodeGenerator;
        }
        


        [MiddlewareFilter(typeof(JsReportPipeline))]
        [Route("purchaseOrder/{id}")]
        public async Task<IActionResult> Index(Guid id, FileFormat format = FileFormat.Pdf, CancellationToken cancellationToken = default)
        {
            SetFormat(format);


            var getOption = await _purchaseOrderService.GetOneAsync(id, cancellationToken);

            return getOption.Match<IActionResult>(
                f0: (purchaseOrder) =>
                {
                    var reportDto = _mapper.Map<ReportDto.PurchaseOrder>(purchaseOrder);
                    reportDto.Barcode = _barcodeGenerator.GenerateBarcode(purchaseOrder.Barcode);
                    return View(reportDto);
                },
                f1: notFound => NotFound()
            );
        }
    }
}
