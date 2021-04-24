using AutoMapper;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.PurchaseOrderAgg;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Core;

namespace CA.ERP.DataAccess.Repositories
{
    public class PurchaseOrderRepository : AbstractRepository<PurchaseOrder, Dal.PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PurchaseOrder> GetByIdWithPurchaseOrderItemsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            PurchaseOrder ret = null;

            var queryable = _context.Set<Dal.PurchaseOrder>().Include(po =>po.PurchaseOrderItems).AsQueryable().AsNoTracking();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<PurchaseOrder>(entity);
            }
            return ret;
        }



        public async Task<int> CountAsync(Guid? branchId, string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, CancellationToken cancellationToken)
        {
            var query = _context.PurchaseOrders.AsQueryable();
            if (branchId != null)
            {
                query = query.Where(po => po.DestinationBranchId == branchId);
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                query = query.Where(po => po.Barcode.StartsWith(barcode));
            }
            if (startDate != null)
            {
                var startDateValue = startDate.Value;
                query = query.Where(po => po.DeliveryDate >= startDateValue);
            }
            if (endDate != null)
            {
                var endDateValue = endDate.Value;
                query = query.Where(po => po.DeliveryDate <= endDateValue);
            }
            return await query.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetManyAsync(Guid? branchId, string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, int skip, int take, CancellationToken cancellationToken)
        {
            var query = _context.PurchaseOrders.Include(po => po.Supplier).Include(po => po.Branch).AsQueryable();
            if (branchId != null)
            {
                query = query.Where(po => po.DestinationBranchId == branchId);
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                query = query.Where(po => po.Barcode.StartsWith(barcode));
            }
            if (startDate != null)
            {
                var startDateValue = startDate.Value;
                query = query.Where(po => po.DeliveryDate >= startDateValue);
            }
            if (endDate != null)
            {
                var endDateValue = endDate.Value;
                query = query.Where(po => po.DeliveryDate <= endDateValue);
            }
            return await query.OrderByDescending(po => po.DeliveryDate).Skip(skip).Take(take).Select(po => _mapper.Map<PurchaseOrder>(po)).ToListAsync(cancellationToken);
        }

    public async Task<OneOf<PurchaseOrder, None>> GetByBarocdeAsync(string purchaseOrderBarcode, CancellationToken cancellationToken)
    {
      OneOf<PurchaseOrder, None> ret = default(None);

            var queryable = _context.Set<Dal.PurchaseOrder>()
                .Include(po => po.Supplier)
                .Include(po => po.Branch)
                .Include(po => po.PurchaseOrderItems).ThenInclude(pois => pois.MasterProduct).ThenInclude(mp => mp.Brand)
                .AsQueryable();
            var entity = await queryable.Where(po => po.Barcode == purchaseOrderBarcode).Select(po => _mapper.Map<PurchaseOrder>(po)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = entity;
            }
            return ret;
    }
  }
}
