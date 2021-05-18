using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<bool> StockNumberExist(string stockNumber, Guid exludeId = default);
        Task<bool> SerialNumberExist(string serialNumber, Guid exludeId = default);
        Task<int> CountAsync(Guid? brandId, Guid? masterProductId, string stockNumber, string serial, StockStatus? StockStatus, CancellationToken cancellationToken = default);
        Task<IEnumerable<Stock>> GetManyAsync(Guid? brandId, Guid? masterProductId, string stockNumber, string serial, StockStatus? StockStatus, int skip, int take, CancellationToken cancellationToken = default);
        Task<List<Stock>> GetManyAsync(Guid branchId, List<Guid> stockIds, CancellationToken cancellationToken = default);
    }
}
