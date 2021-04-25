using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockReceiveRepository : AbstractRepository<StockReceive, Dal.StockReceive>, IStockReceiveRepository
    {
        public StockReceiveRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task UpdateAsync(Guid id, StockReceive entity, CancellationToken cancellationToken = default)
        {
            var dalEntity = await _context.Set<Dal.StockReceive>().FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
            if (dalEntity != null)
            {
                _mapper.Map(entity, dalEntity);

                _context.Entry(dalEntity).State = EntityState.Modified;
                foreach (var item in dalEntity.Items)
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
        }

        public async Task<List<StockReceive>> GetManyStockReceiveAsync(Guid? branch, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, int skip, int take, CancellationToken cancellationToken)
        {
            IQueryable<Dal.StockReceive> queryable = generateQuery(branch, supplierId, dateCreated, dateReceived);

            return await queryable.Include(sr => sr.Branch).Include(sr=>sr.Supplier).OrderByDescending(sr => sr.DateCreated).Skip(skip).Take(take).Select(e => _mapper.Map<Dal.StockReceive, StockReceive>(e)).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }

        private IQueryable<Dal.StockReceive> generateQuery(Guid? branch, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived)
        {
            var queryable = _context.Set<Dal.StockReceive>().AsQueryable();
            if (branch != null)
            {
                queryable = queryable.Where(e => e.BranchId == branch.Value);
            }
            if (supplierId != null)
            {
                queryable = queryable.Where(e => e.SupplierId == supplierId.Value);
            }
            if (dateCreated != null)
            {
                var startDate = dateCreated.Value.Date;
                var endDate = dateCreated.Value.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(e => e.DateCreated >= startDate && e.DateCreated <= endDate);
            }
            if (dateReceived != null)
            {
                var startDate = dateReceived.Value.Date;
                var endDate = dateReceived.Value.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(e => e.DateReceived >= startDate && e.DateReceived <= endDate);
            }

            return queryable;
        }

        public Task<int> GetManyStockReceiveCountAsync(Guid? branch, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, CancellationToken cancellationToken)
        {
            IQueryable<Dal.StockReceive> queryable = generateQuery(branch, supplierId, dateCreated, dateReceived);
            return queryable.CountAsync(cancellationToken);
        }

        public async Task<StockReceive> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken)
        {
            StockReceive ret = null;

            var queryable = _context.Set<Dal.StockReceive>().AsQueryable()
                .Include(sr =>sr.Supplier).Include(sr => sr.Branch)
                .Include(sr => sr.Items).ThenInclude(i => i.MasterProduct).ThenInclude(i => i.Brand)
                .AsNoTracking();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<StockReceive>(entity);
            }
            return ret;
        }
    }
}
