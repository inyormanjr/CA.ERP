using CA.ERP.Common.Types;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockReceive : EntityBase
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSouce { get; set; }
        public StockReceiveStage Stage { get; private set; }
        public DateTimeOffset? DateReceived { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string DeliveryReference { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Branch Branch { get; set; }

        public List<StockReceiveItem> Items { get; set; } = new List<StockReceiveItem>();
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
