using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveItemView
    {
        public Guid Id { get; set; }

        public StockReceiveItemStatus Status { get; set; }

        public Guid MasterProductId { get;  set; }
        public Guid StockReceiveId { get;  set; }
        public Guid? PurchaseOrderItemId { get;  set; }
        public Guid BranchId { get; private set; }
        public string StockNumber { get;  set; }
        public string SerialNumber { get;  set; }
        public decimal CostPrice { get;  set; }

        public string BrandName { get;  set; }
        public string Model { get;  set; }
    }
}
