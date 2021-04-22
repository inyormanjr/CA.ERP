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

        public async Task<List<StockReceive>> GetManyStockReceiveAsync(Guid? branch, Guid? supplierId, DateTimeOffset? dateReceived, int skip, int take, CancellationToken cancellationToken)
        {
            IQueryable<Dal.StockReceive> queryable = generateQuery(branch, supplierId, dateReceived);

            return await queryable.Include(sr => sr.Branch).Include(sr=>sr.Supplier).OrderByDescending(sr => sr.DateCreated).Skip(skip).Take(take).Select(e => _mapper.Map<Dal.StockReceive, StockReceive>(e)).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }

        private IQueryable<Dal.StockReceive> generateQuery(Guid? branch, Guid? supplierId, DateTimeOffset? dateReceived)
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
            if (dateReceived != null)
            {
                var startDate = dateReceived.Value.Date;
                var endDate = dateReceived.Value.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(e => e.DateReceived >= startDate && e.DateReceived <= endDate);
            }

            return queryable;
        }

        public Task<int> GetManyStockReceiveCountAsync(Guid? branch, Guid? supplierId, DateTimeOffset? dateReceived, CancellationToken cancellationToken)
        {
            IQueryable<Dal.StockReceive> queryable = generateQuery(branch, supplierId, dateReceived);
            return queryable.CountAsync(cancellationToken);
        }

        public async Task<StockReceive> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken)
        {
            StockReceive ret = null;

            var queryable = _context.Set<Dal.StockReceive>().AsQueryable()
                .Include(sr =>sr.Supplier).Include(sr => sr.Branch).Include(sr => sr.Items)
                .Include(sr => sr.Items).ThenInclude(i => i.Branch)
                .Include(sr => sr.Items).ThenInclude(i => i.MasterProduct)
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
