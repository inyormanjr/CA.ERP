using CA.ERP.Domain.Base;
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
        Task<int> CountAsync(string brand, string model, string stockNumber, string serial, CancellationToken cancellationToken = default);
        Task<IEnumerable<Stock>> GetManyAsync(string brand, string model, string stockNumber, string serial, int skip, int take, CancellationToken cancellationToken = default);
        Task<List<Stock>> GetManyAsync(Guid branchId, List<Guid> stockIds, CancellationToken cancellationToken = default);
    }
}
