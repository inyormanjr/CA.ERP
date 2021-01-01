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

namespace CA.ERP.DataAccess.Repositories
{
    public class PurchaseOrderRepository : AbstractRepository<PurchaseOrder, Dal.PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async override Task<OneOf<Guid, None>> UpdateAsync(Guid id, PurchaseOrder entity, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> result = default(None);
            var dalEntity = await _context.PurchaseOrders.FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
            if (dalEntity != null)
            {
                _mapper.Map(entity, dalEntity);
                dalEntity.Id = id;
                _context.Entry(dalEntity).State = EntityState.Modified;
                _context.Entry(dalEntity).Property(t => t.Barcode).IsModified = false;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                result = dalEntity.Id;
            }

            return result;
        }

        public async override Task<List<PurchaseOrder>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            var queryable = _context.PurchaseOrders.Include(po=>po.Supplier).Include(po=>po.Branch).AsQueryable();
            if (status != Status.All)
            {
                var dalStatus = (DataAccess.Common.Status)status;
                queryable = queryable.Where(e => e.Status == dalStatus);
            }


            return await queryable.Select(e => _mapper.Map<PurchaseOrder>(e)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
