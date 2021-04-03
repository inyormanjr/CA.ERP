using AutoMapper;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.SupplierAgg;
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
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Core;

namespace CA.ERP.DataAccess.Repositories
{
    public class SupplierRepository : AbstractRepository<Supplier, Dal.Supplier>, ISupplierRepository
    {

        public SupplierRepository(CADataContext context, IMapper mapper):base(context, mapper)
        {

        }
        public async override Task<OneOf<Supplier, None>> GetByIdAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            OneOf<Supplier, None> ret = default(None);

            var queryable = _context.Suppliers.Include(s=>s.SupplierMasterProducts).Include(s=>s.SupplierBrands).ThenInclude(sb=>sb.Brand).AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<Supplier>(entity);
            }
            return ret;
        }


        public async Task<OneOf<Success, None>> AddSupplierBrandAsync(Guid supplierId, SupplierBrand supplierBrand, CancellationToken cancellationToken)
        {
            OneOf<Success, None> ret = default(None);
            var supplierExist = await _context.Suppliers.AnyAsync(s => s.Id == supplierId, cancellationToken);
            if (supplierExist)
            {
                var supplierBrandExist = _context.SupplierBrands.Any(sb =>sb.SupplierId == supplierId && sb.BrandId == supplierBrand.BrandId);

                //if not exist add else do nothing and return success
                if (!supplierBrandExist)
                {
                    var dalSupplierBrand = new Dal.SupplierBrand()
                    {
                        BrandId = supplierBrand.BrandId,
                        SupplierId = supplierId
                    };
                    await _context.SupplierBrands.AddAsync(dalSupplierBrand);
                }
                
                ret = default(Success);
            }
            return ret;
        }

        public async Task<OneOf<Success, None>> DeleteSupplierBrandAsync(Guid supplierId, Guid brandId, CancellationToken cancellationToken)
        {
            OneOf<Success, None> ret = default(None);
            var supplierBrand = await _context.SupplierBrands.FirstOrDefaultAsync(sb => sb.SupplierId == supplierId && sb.BrandId == brandId);
            if (supplierBrand != null)
            {
                _context.Entry(supplierBrand).State = EntityState.Deleted;
                ret = default(Success);
            }
            return ret;
        }

        public Task<List<SupplierBrandLite>> GetSupplierBrandsAsync(Guid supplierId, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            var queryable = _context.SupplierBrands.AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }
            var supplierBrands = queryable.Where(sb => sb.SupplierId == supplierId)
                .Select(sb => 
                    new SupplierBrandLite() { 
                        SupplierId = sb.SupplierId,
                        BrandId = sb.BrandId, 
                        BrandName = sb.Brand.Name, 
                        MasterProducts = sb.Brand.MasterProducts.Select(
                            mp => new SupplierMasterProductLite() { 
                                SupplierId = sb.SupplierId,
                                MasterProductId = mp.Id, 
                                Model = mp.Model, 
                                CostPrice = mp.SupplierMasterProducts.Where(smp=>smp.SupplierId == sb.SupplierId).Select(smp=>smp.CostPrice).FirstOrDefault() 
                            })  
                    }).ToListAsync();

            return supplierBrands;
        }

        public async Task AddOrUpdateSupplierMasterProductCostPriceAsync(Guid supplierId, Guid masterProductId, decimal costPrice, CancellationToken cancellationToken = default)
        {
            var supplierMasterProduct = await _context.SupplierMasterProducts.FirstOrDefaultAsync(smp => smp.SupplierId == supplierId && smp.MasterProductId == masterProductId, cancellationToken);
            if (supplierMasterProduct == null)
            {
                supplierMasterProduct = new Dal.SupplierMasterProduct() { 
                    SupplierId = supplierId,
                    MasterProductId = masterProductId,
                };
                _context.SupplierMasterProducts.Add(supplierMasterProduct);
            }

            supplierMasterProduct.CostPrice = costPrice;
        }
    }
}
