using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.WebApp.CustomAuthentication;
using CA.ERP.WebApp.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.StockReceive
{
    public class StockReceiveCreate 
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSource { get; set; }
        public Guid SupplierId { get; set; }

        public List<StockCreate> Stocks { get; set; } = new List<StockCreate>();
        public string DeliveryReference { get; set; }
    }
}
