using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.ReportDto
{
    public partial class StockList
    {

        public string BranchName { get; set; }


        public string BranchContact { get; set; }

        public DateTimeOffset Date { get; set; }

        public List<StockListItem> Stocks { get; set; }
    }
}
