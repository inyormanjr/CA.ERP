using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        Task<int> CountAsync(Guid? branchId, string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, CancellationToken cancellationToken = default);
        Task<PurchaseOrder> GetByIdWithPurchaseOrderItemsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PurchaseOrder>> GetManyAsync(Guid? branchId, string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, int skip, int take, CancellationToken cancellationToken = default);
    }
}
