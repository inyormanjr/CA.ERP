﻿using AutoMapper;
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

        public async Task<int> CountAsync(string brand, string model, string stockNumber, string serial, CancellationToken cancellationToken = default)
        {
            var query = _context.Stocks.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(s => s.MasterProduct.Brand.Name.StartsWith(brand));
            }
            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(s => s.MasterProduct.Model.StartsWith(model));
            }
            if (!string.IsNullOrEmpty(stockNumber))
            {
                query = query.Where(s => s.StockNumber.StartsWith(stockNumber));
            }
            if (!string.IsNullOrEmpty(serial))
            {
                query = query.Where(s => s.SerialNumber.StartsWith(serial));
            }
            return await query.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Stock>> GetManyAsync(string brand, string model, string stockNumber, string serial, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = _context.Stocks.Include(s=>s.MasterProduct).ThenInclude(m=>m.Brand).AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(s => s.MasterProduct.Brand.Name.StartsWith(brand));
            }
            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(s => s.MasterProduct.Model.StartsWith(model));
            }
            if (!string.IsNullOrEmpty(stockNumber))
            {
                query = query.Where(s => s.StockNumber.StartsWith(stockNumber));
            }
            if (!string.IsNullOrEmpty(serial))
            {
                query = query.Where(s => s.SerialNumber.StartsWith(serial));
            }
            return await query.Skip(skip).Take(take).Select(s => _mapper.Map<Stock>(s)).ToListAsync(cancellationToken);
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
