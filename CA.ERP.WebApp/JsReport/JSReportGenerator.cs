using CA.ERP.Domain.ReportAgg;
using jsreport.Shared;
using jsreport.Types;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.JsReport
{
    public class JSReportGenerator : IReportGenerator
    {
        private readonly IRenderService _renderService;
        private Dictionary<string, string> _reportLocations = new Dictionary<string, string>();

        public JSReportGenerator(IRenderService renderService)
        {
            _renderService = renderService;
            _reportLocations.Add("PurchaseOrder", "JsReport/data/purchase-orders/purchase-order/report/content.handlebars");
            _reportLocations.Add("StockList", "JsReport/data/stocks/stock-list/report/content.handlebars");
        }
        public async Task<OneOf<Domain.ReportAgg.Report, None>> GenerateReport(string reportName, object data, CancellationToken cancellationToken = default)
        {
            OneOf<Domain.ReportAgg.Report, None> ret = default(None);
            if (_reportLocations.ContainsKey(reportName))
            {
                string content = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "JsReport/data/purchase-orders/purchase-order/report/content.handlebars"));

                var report = await _renderService.RenderAsync(new RenderRequest()
                {
                    Template = new Template()
                    {
                        Recipe = Recipe.ChromePdf,
                        Engine = Engine.Handlebars,
                        Content = content
                    },
                    Data = data
                }, cancellationToken);
                ret = new Domain.ReportAgg.Report()
                {
                    Content = report.Content,
                    ContentType = report.Meta.ContentType,
                    FileExtension = report.Meta.FileExtension
                };
            }
            return ret;
            
        }
    }
}
