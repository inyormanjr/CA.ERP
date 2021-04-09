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



        

        public async Task<int> CountAsync(string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, CancellationToken cancellationToken)
        {
            var query = _context.PurchaseOrders.AsQueryable();
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

        public async Task<IEnumerable<PurchaseOrder>> GetManyAsync(string barcode, DateTimeOffset? startDate, DateTimeOffset? endDate, int skip, int take, CancellationToken cancellationToken)
        {
            var query = _context.PurchaseOrders.Include(po => po.Supplier).Include(po => po.Branch).AsQueryable();
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

    }
}
