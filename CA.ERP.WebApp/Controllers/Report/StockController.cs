using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.ReportAgg;
using CA.ERP.Domain.StockAgg;
using jsreport.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Report
{
    [Authorize]
    public class StockController : BaseReportController
    {
        private readonly BranchService _branchService;
        private readonly StockService _stockService;
        private readonly IMapper _mapper;

        public StockController(IServiceProvider serviceProvider, BranchService branchService, StockService stockService, IMapper mapper) 
            : base(serviceProvider)
        {
            _branchService = branchService;
            _stockService = stockService;
            _mapper = mapper;
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery]Guid branchId, [FromQuery] List<Guid> stockIds, FileFormat format = FileFormat.Pdf, CancellationToken cancellationToken = default)
        {
            SetFormat(format);

            var branchOption = await _branchService.GetOneAsync(branchId);
            return await branchOption.Match<Task<IActionResult>>(
                f0: async branch => {
                    List<Stock> stocks = await _stockService.GetManyAsync(branchId, stockIds.ToList());


                    var reportDto = new ReportDto.StockList();
                    reportDto.BranchContact = branch.Contact;
                    reportDto.BranchName = branch.Name;
                    reportDto.Date = DateTime.Now;
                    reportDto.Stocks = _mapper.Map<List<ReportDto.StockListItem>>(stocks);
                    return View(reportDto);
                },
                f1: async _ => NotFound()
                );
        }
    }
}
