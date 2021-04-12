using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Reporting.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly CADataContext _cADataContext;

        public PurchaseOrderRepository(CADataContext cADataContext)
        {
            _cADataContext = cADataContext;
        }
        public Task<PurchaseOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _cADataContext.PurchaseOrders.Include(po => po.PurchaseOrderItems).FirstOrDefaultAsync(po => po.Id == id, cancellationToken);
        }
    }
}
