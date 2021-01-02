using AspNetCore.Reporting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.ReportAgg
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly Dictionary<RenderType, string> _mimeTypes;

        public ReportGenerator()
        {
            _mimeTypes = new Dictionary<RenderType, string>();
            _mimeTypes.Add(RenderType.Pdf, "application/pdf");
            _mimeTypes.Add(RenderType.Word, "application/msword");
            _mimeTypes.Add(RenderType.WordOpenXml, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            _mimeTypes.Add(RenderType.Excel, "application/vnd.ms-excel");
            _mimeTypes.Add(RenderType.ExcelOpenXml, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            _mimeTypes.Add(RenderType.Image, "image/jpeg");
            _mimeTypes.Add(RenderType.Html, "text/html");
            _mimeTypes.Add(RenderType.Rpl, "application/rpl");
        }
        public ReportResult GenerateReport(string path, RenderType renderType = RenderType.Pdf, Dictionary<string, string> parameters = null, Dictionary<string, object> dataSources = null)
        {
            LocalReport localReport = new LocalReport(path);
            if (dataSources != null)
            {
                foreach (var dataSource in dataSources)
                {
                    localReport.AddDataSource(dataSource.Key, dataSource.Value);
                }
            }
            return localReport.Execute(renderType, 1, parameters);
        }

        public string GetMimeTypeFor(RenderType renderType)
        {
            var mimeType = string.Empty;
            _mimeTypes.TryGetValue(renderType, out  mimeType);
            return mimeType;
            
        }
    }
}
