using AutoMapper;
using CA.ERP.Domain.StockMoveAgg;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockMoveRepository :  IStockMoveRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public StockMoveRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OneOf<StockMove, None>> GetLatestStockMoveAsync(Guid masterProductId, Guid branchId, CancellationToken cancellationToken)
        {
            OneOf<StockMove, None> ret = default(None);
            var latestStockMove = await _context.StockMoves.Where(sm => sm.MasterProductId == masterProductId && sm.BranchId == branchId).OrderByDescending(sm => sm.MoveDate).FirstOrDefaultAsync(cancellationToken);
            if (latestStockMove != null)
            {
                ret = _mapper.Map<StockMove>(latestStockMove);
            }
            return ret;
        }
    }
}
