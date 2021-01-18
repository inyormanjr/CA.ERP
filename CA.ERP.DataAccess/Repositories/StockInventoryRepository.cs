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
                //set entity state to modified because for some reason auto mapper sets the state to deleted.
                _context.Entry(dalStockInventory).State = EntityState.Modified;
            }
            else
            {
                dalStockInventory = _mapper.Map<Dal.StockInventory>(stockInventory);
                _context.StockInventories.Add(dalStockInventory);
                
            }

        }

        public async Task<OneOf<StockInventory, None>> GetOneNoTrackingAsync(Guid masterProductId, Guid branchId)
        {
            OneOf<StockInventory, None> ret = default(None);
            var dalStockInventory = await _context.StockInventories.AsNoTracking().FirstOrDefaultAsync(s => s.MasterProductId == masterProductId && s.BranchId == branchId);
            if (dalStockInventory != null)
            {
                //clear branch
                ret = _mapper.Map<StockInventory>(dalStockInventory);
            }
            return ret;
        }
    }
}
