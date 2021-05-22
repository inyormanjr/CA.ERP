using AutoMapper;
using CA.ERP.Common.Types;
using CA.ERP.Domain.StockAgg;
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
    public class StockRepository : AbstractRepository<Stock, Dal.Stock>, IStockRepository
    {
        public StockRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> CountAsync(Guid? branchId, Guid? brandId, Guid? masterProductId, string stockNumber, string serial, StockStatus? stockStatus, CancellationToken cancellationToken = default)
        {
            var query = _context.Stocks.AsQueryable();

            query = generateQuery(query, branchId, brandId, masterProductId, stockNumber, serial, stockStatus);
            return await query.CountAsync(cancellationToken);
        }

        private static IQueryable<Dal.Stock> generateQuery(IQueryable<Dal.Stock> query, Guid? branchId, Guid? brandId, Guid? masterProductId, string stockNumber, string serial, StockStatus? stockStatus)
        {
            if (branchId != null)
            {
                query = query.Where(s => s.BranchId == branchId);
            }
            if (brandId != null)
            {
                query = query.Where(s => s.MasterProduct.Brand.Id == brandId);
            }
            if (masterProductId != null)
            {
                query = query.Where(s => s.MasterProduct.Id == masterProductId);
            }
            if (!string.IsNullOrEmpty(stockNumber))
            {
                query = query.Where(s => s.StockNumber.StartsWith(stockNumber));
            }
            if (!string.IsNullOrEmpty(serial))
            {
                query = query.Where(s => s.SerialNumber.StartsWith(serial));
            }
            if (stockStatus != null)
            {
                query = query.Where(s => s.StockStatus == stockStatus);
            }
            return query;
        }

        public async Task<IEnumerable<Stock>> GetManyAsync(Guid? branchId, Guid? brandId, Guid? masterProductId, string stockNumber, string serial, StockStatus? stockStatus, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = _context.Stocks.Include(s => s.Supplier).Include(s => s.Branch).Include(s => s.MasterProduct).ThenInclude(m => m.Brand).AsQueryable();

            query = generateQuery(query, branchId, brandId, masterProductId, stockNumber, serial, stockStatus);

            return await query.OrderByDescending(s => s.CreatedAt).Skip(skip).Take(take).Select(s => _mapper.Map<Stock>(s)).ToListAsync(cancellationToken);
        }

        public async Task<List<Stock>> GetManyAsync(Guid branchId, List<Guid> stockIds, CancellationToken cancellationToken)
        {
            var query = _context.Stocks.Include(s => s.MasterProduct).ThenInclude(m => m.Brand).AsQueryable();

            query = query.Where(s => s.BranchId == branchId && stockIds.Contains(s.Id));

            return await query.OrderBy(s => s.StockNumber).Select(s => _mapper.Map<Stock>(s)).ToListAsync(cancellationToken);
        }

        public async Task<bool> SerialNumberExist(string serialNumber, Guid exludeId = default)
        {
            return await _context.Stocks.AnyAsync(s => s.SerialNumber == serialNumber && s.Id != exludeId);
        }

        public async Task<bool> StockNumberExist(string stockNumber, Guid exludeId = default)
        {
            return await _context.Stocks.AnyAsync(s => s.StockNumber == stockNumber && s.Id != exludeId);
        }
    }
}
