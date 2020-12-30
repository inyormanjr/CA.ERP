using AutoMapper;
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
    }
}
