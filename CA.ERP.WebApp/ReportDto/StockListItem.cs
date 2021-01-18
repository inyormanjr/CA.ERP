using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.ReportDto
{
    public class StockListItem
    {
        public string StockNumber { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }
    }
}
