using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        Task<int> CountAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<PurchaseOrder>> GetManyAsync(DateTime startDate, DateTime endDate, int skip, int take, CancellationToken cancellationToken = default);
    }
}
