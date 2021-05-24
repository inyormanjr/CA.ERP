using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockTransfer : EntityBase
    {

        public Guid SourceBranchId { get; set; }

        public Guid DestinationBranchId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public ICollection<StockTransferItem> Items { get; set; } = new List<StockTransferItem>();

        public Branch SourceBranch { get; set; }

        public Branch DestinationBranch { get; set; }

    }
}
