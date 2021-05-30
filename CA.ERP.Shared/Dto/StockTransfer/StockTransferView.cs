using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferView
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public Guid SourceBranchId { get; set; }

        public string SourceBranchName { get; set; }

        public Guid DestinationBranchId { get;  set; }

        public string DestinationBranchName { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public IEnumerable<StockTransferItemView> Items { get; set; } = Enumerable.Empty<StockTransferItemView>();
    }
}
