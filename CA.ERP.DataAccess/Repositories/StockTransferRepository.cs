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

        public async override Task<List<StockTransfer>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            IQueryable<Dal.StockTransfer> queryable = _context.StockTransfers.Include(st => st.SourceBranch).Include(st => st.DestinationBranch).AsQueryable();
            queryable = generateQuery(queryable, status);


            return await queryable.OrderBy(st => st.CreatedAt).Skip(skip).Take(take).AsNoTracking().Select(e => _mapper.Map<StockTransfer>(e)).ToListAsync(cancellationToken: cancellationToken);
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
