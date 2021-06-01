using CA.ERP.Common.Types;
using CA.ERP.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockTransferAgg
{
    public interface IStockTransferRepository : IRepository<StockTransfer>
    {
        Task<int> GetCountAsync(string number, StockTransferStatus? stockTransferStatus, Status status = Status.Active, CancellationToken cancellationToken = default);

        Task<List<StockTransfer>> GetManyAsync(string number, StockTransferStatus? stockTransferStatus, int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default);
    }
}
