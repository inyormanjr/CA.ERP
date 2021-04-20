using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.Repository;
using CA.ERP.Domain.StockCounterAgg;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public interface IStockCounterRepository : IRepository
    {
        Task<StockCounter> GetStockCounterAsync(string code, CancellationToken cancellationToken = default);

        Task AddOrUpdateStockCounterAsync(StockCounter stockCounter, CancellationToken cancellationToken = default);

    }
}
