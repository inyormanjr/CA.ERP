using AutoMapper;
using CA.ERP.Common.Types;
using CA.ERP.Domain.MasterProductAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dto = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class MasterProductRepository : AbstractRepository<MasterProduct, Dto.MasterProduct>, IMasterProductRepository
    {
        public MasterProductRepository(CADataContext context, IMapper mapper)
            : base(context, mapper)
        {

        }

        public async Task<List<MasterProduct>> GetManyAsync(string model, int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {

            var query = _context.MasterProducts.AsQueryable();
            query = generateQuery(query, model, status);

            return await query.OrderBy(mp => mp.Model).Skip(skip).Take(take).Select(mp => _mapper.Map<MasterProduct>(mp)).ToListAsync();
        }

        private static IQueryable<Dto.MasterProduct> generateQuery(IQueryable<Dto.MasterProduct> query, string model, Status status)
        {
            if (status != Status.All)
            {
                query = query.Where(mp => mp.Status == status);
            }

            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(mp => mp.Model.ToLower().StartsWith(model.ToLower()));
            }

            return query;
        }

        public async Task<int> GetCountAsync(string model, Status status = Status.Active, CancellationToken cancellationToken = default)
        {

            var query = _context.MasterProducts.AsQueryable();
            query = generateQuery(query, model, status);

            return await query.CountAsync();
        }

        public async Task<MasterProduct> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            MasterProduct ret = null;

            var queryable = _context.MasterProducts.AsQueryable();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<MasterProduct>(entity);
            }
            return ret;
        }

        public async Task<List<MasterProduct>> GetManyWithBrandAndSupplierAsync(Guid brandId, Guid supplierId, CancellationToken cancellationToken)
        {
            var masterProducts = await _context.SupplierMasterProducts.Where(smp => smp.MasterProduct.BrandId == brandId && smp.SupplierId == supplierId).Select(smp => smp.MasterProduct).ToListAsync();
            return _mapper.Map<List<MasterProduct>>(masterProducts);
        }
    }
}
