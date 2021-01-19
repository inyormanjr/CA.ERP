using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public interface IStockInventoryRepository : IRepository
    {
        Task<OneOf<StockInventory, None>> GetOneNoTrackingAsync(Guid masterProductId, Guid branchId);
        Task AddOrUpdateAsync(StockInventory stockInventory, CancellationToken cancellationToken = default);
    }
}
