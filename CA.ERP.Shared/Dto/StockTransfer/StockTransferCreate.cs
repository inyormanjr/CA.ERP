using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferCreate
    {
        public Guid Id { get;  set; }

        public Guid SourceBranchId { get;  set; }

        public string SourceBranchName { get; set; }

        public Guid DestinationBranchId { get;  set; }

        public string DestinationBranchName { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public ICollection<StockTransferItemCreate> Items { get; set; } = new List<StockTransferItemCreate>();

    }
}
