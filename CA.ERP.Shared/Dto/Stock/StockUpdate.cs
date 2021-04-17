using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.Stock
{
    /// <summary>
    /// Contract to update stock
    /// </summary>
    public class StockUpdate
    {
        public Guid MasterProductId { get; set; }
        public string SerialNumber { get; set; }
        public decimal CostPrice { get; set; }
    }
}
