using CA.ERP.Domain.Base;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockAgg
{
    public class Stock:ModelBase
    {
        public Stock()
        {
            StockStatus = StockStatus.Available;
        }

        public Guid MasterProductId { get; set; }
        public Guid StockReceiveId { get; set; }
        public Guid? PurchaseOrderItemId { get; set; }
        public string StockNumber { get; set; }
        public string SerialNumber { get; set; }
        public StockStatus StockStatus { get; set; }
        public decimal CostPrice { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public StockReceive StockReceive { get; set; }
        public PurchaseOrderItem PurchaseOrderItem { get; set; }
    }
}
