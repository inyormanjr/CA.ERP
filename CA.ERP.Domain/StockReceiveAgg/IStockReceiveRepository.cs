using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public interface IStockReceiveRepository : IRepository<StockReceive>
    {
        Task<List<StockReceive>> GetManyStockReceiveAsync(Guid? branch, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, StockSource? source, StockReceiveStage? stage, int skip, int take, CancellationToken cancellationToken);
        Task<int> GetManyStockReceiveCountAsync(Guid? branch, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, StockSource? source, StockReceiveStage? stage, CancellationToken cancellationToken);
        Task<StockReceive> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken);
    }
}
