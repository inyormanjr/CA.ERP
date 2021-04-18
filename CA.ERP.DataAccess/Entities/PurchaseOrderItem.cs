using CA.ERP.Domain.PurchaseOrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class PurchaseOrderItem : EntityBase
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid MasterProductId { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal DeliveredQuantity { get; set; }
        public PurchaseOrderItemStatus PurchaseOrderItemStatus { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public MasterProduct MasterProduct { get; set; }
        //public List<Stock> Stocks { get;  set; }
        public List<StockReceiveItem> StockReceiveItems { get;  set; }
    }
}
