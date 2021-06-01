using AutoMapper;
using CA.ERP.Common.Types;
using CA.ERP.Domain.StockTransferAgg;
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
    public class StockTransferRepository : AbstractRepository<StockTransfer, Dal.StockTransfer>, IStockTransferRepository
    {
        public StockTransferRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        private IQueryable<Dal.StockTransfer> generateQuery(IQueryable<Dal.StockTransfer> queryable, string number, StockTransferStatus? stockTransferStatus, Status status)
        {
            if (!string.IsNullOrEmpty(number))
            {
                queryable = queryable.Where(e => e.Number.ToLower().StartsWith(number.ToLower()));
            }
            if (stockTransferStatus != null)
            {
                queryable = queryable.Where(e => e.StockTransferStatus == stockTransferStatus);
            }
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }

            return queryable;
        }

        public async Task<List<StockTransfer>> GetManyAsync(string number, StockTransferStatus? stockTransferStatus, int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            IQueryable<Dal.StockTransfer> queryable = _context.StockTransfers.Include(st => st.SourceBranch).Include(st => st.DestinationBranch).AsQueryable();
            queryable = generateQuery(queryable, number, stockTransferStatus, status);


            return await queryable.OrderBy(st => st.CreatedAt).Skip(skip).Take(take).AsNoTracking().Select(e => _mapper.Map<StockTransfer>(e)).ToListAsync(cancellationToken: cancellationToken);
        }

        public Task<int> GetCountAsync(string number, StockTransferStatus? stockTransferStatus, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            IQueryable<Dal.StockTransfer> queryable = _context.Set<Dal.StockTransfer>().AsQueryable();
            queryable = generateQuery(queryable, number, stockTransferStatus, status);
            return queryable.CountAsync(cancellationToken);
        }

        public async override Task<StockTransfer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            StockTransfer ret = null;

            var queryable = _context.Set<Dal.StockTransfer>().Include(st => st.SourceBranch).Include(st => st.DestinationBranch).Include(st => st.Items).ThenInclude(sti => sti.MasterProduct).ThenInclude(mp => mp.Brand).AsQueryable().AsNoTracking();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<StockTransfer>(entity);
            }
            return ret;
        }

    }
}
