using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public interface IStockInventoryRepository : IRepository<StockInventory>
    {
        Task<OneOf<StockInventory, None>> GetOneAsync(Guid masterProductId, Guid branchId);
    }
}
