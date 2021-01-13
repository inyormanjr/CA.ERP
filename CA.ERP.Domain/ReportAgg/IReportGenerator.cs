using AspNetCore.Reporting;
using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.ReportAgg
{
    public interface IReportGenerator : IHelper
    {
        string GetMimeTypeFor(RenderType renderType);
        ReportResult GenerateReport(string path, RenderType renderType = RenderType.Pdf, Dictionary<string, string> parameters = null, Dictionary<string, object> dataSources = null);
    }
}
