using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveView
    {
        public Guid Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSource { get; set; }
        public Guid SupplierId { get; set; }
        public StockReceiveStage Stage { get;  set; }
        public DateTimeOffset? DateReceived { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string SupplierName { get; set; }
        public string BranchName { get; set; }


        public string DeliveryReference { get; set; }
        public ICollection<StockReceiveItemView> Items { get; set; } = new List<StockReceiveItemView>();
    }
}
