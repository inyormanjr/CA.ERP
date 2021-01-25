using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CA.ERP.Domain.ReportAgg
{
    public class Report
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
        public string FileExtension { get; set; }
    }
}
