using AutoMapper;
using CA.ERP.Domain.StockAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockRepository : AbstractRepository<Stock, Dal.Stock>, IStockRepository
    {
        public StockRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<bool> SerialNumberExist(string serialNumber)
        {
            return await _context.Stocks.AnyAsync(s => s.SerialNumber == serialNumber);
        }

        public async Task<bool> StockNumberExist(string stockNumber)
        {
            return await _context.Stocks.AnyAsync(s => s.StockNumber == stockNumber);
        }
    }
}
