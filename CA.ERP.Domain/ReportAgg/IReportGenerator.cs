using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.ReportAgg
{
    public interface IReportGenerator : IHelper
    {
        Task<OneOf<Report, None>> GenerateReport(string reportName, object data, CancellationToken cancellationToken = default);
    }
}
