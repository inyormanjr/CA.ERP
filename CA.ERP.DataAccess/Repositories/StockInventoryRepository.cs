using AutoMapper;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.StockInventoryAgg;
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
    public class StockInventoryRepository : AbstractRepository<StockInventory, Dal.StockInventory> , IStockInventoryRepository
    {
        public StockInventoryRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OneOf<StockInventory, None>> GetOneAsync(Guid masterProductId, Guid branchId)
        {
            OneOf<StockInventory, None> ret = default(None);
            var dalStockInventory = await _context.StockInventories.FirstOrDefaultAsync(s => s.MasterProductId == masterProductId && s.BranchId == branchId);
            if (dalStockInventory != null)
            {
                ret = _mapper.Map<StockInventory>(dalStockInventory);
            }
            return ret;
        }
    }
}
