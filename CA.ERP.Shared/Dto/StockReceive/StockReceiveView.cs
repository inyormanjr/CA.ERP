using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveView
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSource { get; set; }
        public Guid SupplierId { get; set; }
        public DateTimeOffset? DateReceived { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string SupplierName { get; set; }
        public string BranchName { get; set; }


        public string DeliveryReference { get; set; }
        //public List<StockReceiveItemView> Stocks { get; set; } = new List<StockReceiveItemView>();
    }
}
