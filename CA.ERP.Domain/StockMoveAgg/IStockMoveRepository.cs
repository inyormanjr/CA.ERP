using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockMoveAgg
{
    public interface IStockMoveRepository : IRepository
    {
        Task<OneOf<StockMove, None>> GetLatestStockMoveAsync(Guid masterProductId, Guid branchId, CancellationToken cancellationToken = default);
    }
}
