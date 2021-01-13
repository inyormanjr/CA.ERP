using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public class StockInventoryFactory : IStockInventoryFactory
    {
        public StockInventory Create(Guid masterProductId, Guid branchId)
        {
            return new StockInventory() { MasterProductId = masterProductId, BranchId = branchId, Quantity = 0 };
        }
    }
}
