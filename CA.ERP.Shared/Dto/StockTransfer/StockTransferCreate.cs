using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferCreate
    {
        public Guid Id { get; private set; }

        public Guid SourceBranchId { get; private set; }

        public string SourceBranchName { get; set; }

        public Guid DestinationBranchId { get; private set; }

        public string DestinationBranchName { get; set; }

        public ICollection<StockTransferItemCreate> Items { get; set; } = new List<StockTransferItemCreate>();

    }
}
