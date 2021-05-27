using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferView
    {
        public Guid Id { get; private set; }

        public Guid SourceBranchId { get; private set; }

        public string SourceBranchName { get; set; }

        public Guid DestinationBranchId { get; private set; }

        public string DestinationBranchName { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public Guid CreatedBy { get; private set; }

        public string CreatedByName { get; private set; }

        public IEnumerable<StockTransferItemView> Items { get; set; } = Enumerable.Empty<StockTransferItemView>();
    }
}
