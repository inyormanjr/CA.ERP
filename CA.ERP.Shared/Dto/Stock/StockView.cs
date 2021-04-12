using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Stock
{
    public class StockView : DtoViewBase
    {
        public Guid MasterProductId { get; set; }
        public string StockNumber { get; set; }
        public string SerialNumber { get; set; }
        public StockStatus StockStatus { get; set; }
        public decimal CostPrice { get; set; }

        public string BrandName { get; set; }
        public string Model { get; set; }
    }
}
