using CA.ERP.Common.Types;
using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Stock: EntityBase
    {
        public Guid MasterProductId { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus StockStatus { get; set; }

        public decimal CostPrice { get; set; }

        public Guid BranchId { get; set; }

        public Guid SupplierId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public Supplier Supplier { get; set; }

        public Branch Branch { get; set; }

        public MasterProduct MasterProduct { get; set; }

        public List<StockReceiveItem> StockReceiveItems { get; set; } = new List<StockReceiveItem>();
    }
}
