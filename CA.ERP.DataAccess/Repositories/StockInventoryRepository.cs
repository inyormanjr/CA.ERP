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
    public class StockInventoryRepository : IStockInventoryRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public StockInventoryRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrUpdateAsync(StockInventory stockInventory, CancellationToken cancellationToken)
        {
            var dalStockInventory = await _context.StockInventories.FirstOrDefaultAsync(si => si.MasterProductId == stockInventory.MasterProductId && si.BranchId == stockInventory.BranchId, cancellationToken: cancellationToken);
            if (dalStockInventory != null)
            {
                _mapper.Map(stockInventory, dalStockInventory);
            }
            else
            {
                dalStockInventory = _mapper.Map<Dal.StockInventory>(stockInventory);
                _context.StockInventories.Add(dalStockInventory);
            }
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
