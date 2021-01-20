using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.ReportAgg;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.ReportControllers
{
    [Route("reports/[controller]")]
    public class PurchaseOrderController : Controller
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IMapper _mapper;
        private readonly IBarcodeGenerator _barcodeGenerator;
        private readonly IReportGenerator _reportGenerator;
        private readonly IConfiguration _configRoot;

        public PurchaseOrderController(PurchaseOrderService purchaseOrderService, IMapper mapper, IBarcodeGenerator barcodeGenerator, IReportGenerator reportGenerator, IConfiguration configRoot)
        {
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
            _barcodeGenerator = barcodeGenerator;
            _reportGenerator = reportGenerator;
            _configRoot = configRoot;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string webAppUrl = _configRoot.GetSection("WebAppUrl")?.Value ?? "http://webapp/";
            ViewData.Add("WebAppUrl", webAppUrl);
        }


        [MiddlewareFilter(typeof(JsReportPipeline))]
        [Route("{id}")]
        public async Task<IActionResult> Index(Guid id, FileFormat format = FileFormat.Pdf, CancellationToken cancellationToken = default)
        {
            switch (format)
            {
                case FileFormat.Pdf:
                    HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
                    break;
                case FileFormat.Docx:
                    HttpContext.JsReportFeature().Recipe(Recipe.Docx);
                    break;
                case FileFormat.Xlsx:
                    HttpContext.JsReportFeature().Recipe(Recipe.Xlsx);
                    break;
                default:
                    HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
                    break;
            }
            

            var getOption = await _purchaseOrderService.GetOneAsync(id);

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
