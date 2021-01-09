using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.SupplierAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceive:ModelBase
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSouce { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Branch Branch { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
