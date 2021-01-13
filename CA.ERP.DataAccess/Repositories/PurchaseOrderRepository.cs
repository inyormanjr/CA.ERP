﻿using AutoMapper;
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
                result = dalEntity.Id;
            }

            return result;
        }

        public async override Task<List<PurchaseOrder>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            var queryable = _context.PurchaseOrders.Include(po=>po.Supplier).Include(po=>po.Branch).AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }


            return await queryable.OrderBy(po => po.DeliveryDate).Select(e => _mapper.Map<PurchaseOrder>(e)).ToListAsync(cancellationToken: cancellationToken);
        }

        public async override Task<OneOf<PurchaseOrder, None>> GetByIdAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            OneOf<PurchaseOrder, None> ret = default(None);

            var queryable = _context.Set<Dal.PurchaseOrder>()
                .Include(po => po.Supplier)
                .Include(po => po.Branch)
                .Include(po => po.PurchaseOrderItems).ThenInclude(pois => pois.MasterProduct).ThenInclude(mp => mp.Brand)
                .AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }

            var entity = await queryable.Where(po => po.Id == id).Select(po => _mapper.Map<PurchaseOrder>(po)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = entity;
            }
            return ret;
        }

        public async Task<int> CountAsync(string barcode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
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

        public async Task<IEnumerable<PurchaseOrder>> GetManyAsync(string barcode, DateTime? startDate, DateTime? endDate, int skip, int take, CancellationToken cancellationToken)
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
