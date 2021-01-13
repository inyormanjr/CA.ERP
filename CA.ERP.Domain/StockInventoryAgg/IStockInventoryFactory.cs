using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public interface IStockInventoryFactory : IFactory<StockInventory>
    {
        StockInventory Create(Guid masterProductId, Guid branchId);
    }
}
