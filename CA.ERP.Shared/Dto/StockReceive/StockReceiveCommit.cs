using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveCommit
    {
        public Guid Id { get; set; }

        public Guid? PurchaseOrderId { get; set; }

        public Guid BranchId { get; set; }

        public StockSource StockSource { get; set; }

        public StockReceiveStage Stage { get; set; }

        public Guid SupplierId { get; set; }

        public DateTimeOffset? DateReceived { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public string BranchName { get; set; }

        public string DeliveryReference { get; set; }

        [ValidateComplexType]
        public List<StockReceiveItemCommit> Items { get; set; } = new List<StockReceiveItemCommit>();

        public bool IsCommitted()
        {
            return Stage == StockReceiveStage.Commited;
        }
    }
}
