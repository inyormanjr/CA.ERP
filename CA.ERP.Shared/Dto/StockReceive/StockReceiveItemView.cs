using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveItemView
    {
        public Guid Id { get; set; }

        public Status Status { get; set; }

        public Guid MasterProductId { get; private set; }
        public Guid StockReceiveId { get; private set; }
        public Guid? PurchaseOrderItemId { get; private set; }
        public Guid BranchId { get; private set; }
        public string StockNumber { get; private set; }
        public StockStatus StockStatus { get; private set; }
        public string SerialNumber { get; private set; }
        public decimal CostPrice { get; private set; }

        public string BrandName { get; private set; }
        public string Model { get; private set; }
    }
}
