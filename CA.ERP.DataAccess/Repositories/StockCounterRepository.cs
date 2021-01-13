using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Common.Extensions;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockCounterRepository : IStockCounterRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public StockCounterRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrUpdateStockCounterAsync(StockCounter stockCounter, CancellationToken cancellationToken)
        {
            stockCounter.ThrowIfNullArgument(nameof(stockCounter));
            var dalStockCounter = await _context.StockCounters.FirstOrDefaultAsync(sc => sc.Code == stockCounter.Code, cancellationToken);
            if (dalStockCounter != null)
            {
                _mapper.Map(stockCounter, dalStockCounter);
            }
            else
            {
                dalStockCounter = _mapper.Map<Entities.StockCounter>(dalStockCounter);
                _context.StockCounters.Add(dalStockCounter);
            }
        }

        public async Task<OneOf<StockCounter, None>> GetStockCounterAsync(string code, CancellationToken cancellationToken)
        {
            OneOf<StockCounter, None> ret = default(None);
            var stockCounter = await _context.StockCounters.FirstOrDefaultAsync(sc => sc.Code == code,cancellationToken);
            if (stockCounter != null)
            {
                ret = _mapper.Map<StockCounter>(stockCounter);
            }
            return ret;
        }
    }
}
