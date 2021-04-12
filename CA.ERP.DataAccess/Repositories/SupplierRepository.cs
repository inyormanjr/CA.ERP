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
using CA.ERP.Domain.Core.DomainResullts;

namespace CA.ERP.DataAccess.Repositories
{
    public class SupplierRepository : AbstractRepository<Supplier, Dal.Supplier>, ISupplierRepository
    {

        public SupplierRepository(CADataContext context, IMapper mapper):base(context, mapper)
        {

        }
        

        public async Task AddSupplierBrandAsync(Guid supplierId, SupplierBrand supplierBrand, CancellationToken cancellationToken)
        {
            var supplierExist = await _context.Suppliers.AnyAsync(s => s.Id == supplierId, cancellationToken);

            var supplierBrandExist = _context.SupplierBrands.Any(sb => sb.SupplierId == supplierId && sb.BrandId == supplierBrand.BrandId);

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

        }

        public async Task DeleteSupplierBrandAsync(Guid supplierId, Guid brandId, CancellationToken cancellationToken)
        {

            var supplierBrand = await _context.SupplierBrands.FirstOrDefaultAsync(sb => sb.SupplierId == supplierId && sb.BrandId == brandId);
            if (supplierBrand != null)
            {
                _context.Entry(supplierBrand).State = EntityState.Deleted;

            }

        }

        //public Task<List<SupplierBrandLite>> GetSupplierBrandsAsync(Guid supplierId, Status status = Status.Active, CancellationToken cancellationToken = default)
        //{
        //    var queryable = _context.SupplierBrands.AsQueryable();
        //    if (status != Status.All)
        //    {
        //        queryable = queryable.Where(e => e.Status == status);
        //    }
        //    var supplierBrands = queryable.Where(sb => sb.SupplierId == supplierId)
        //        .Select(sb => 
        //            new SupplierBrandLite() { 
        //                SupplierId = sb.SupplierId,
        //                BrandId = sb.BrandId, 
        //                BrandName = sb.Brand.Name, 
        //                MasterProducts = sb.Brand.MasterProducts.Select(
        //                    mp => new SupplierMasterProductLite() { 
        //                        SupplierId = sb.SupplierId,
        //                        MasterProductId = mp.Id, 
        //                        Model = mp.Model, 
        //                        CostPrice = mp.SupplierMasterProducts.Where(smp=>smp.SupplierId == sb.SupplierId).Select(smp=>smp.CostPrice).FirstOrDefault() 
        //                    })  
        //            }).ToListAsync();

        //    return supplierBrands;
        //}

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

        public Task<List<Supplier>> GetManySupplierAsync(string name, int skip, int take, CancellationToken cancellationToken)
        {
            IQueryable<Dal.Supplier> queryable = generateQuery(Status.Active);

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                queryable = queryable.Where(s => s.Name.ToLower().StartsWith(name));
            }

            return queryable.OrderBy(s => s.Name).Skip(skip).Take(take).Select(e => _mapper.Map<Dal.Supplier, Supplier>(e)).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            
        }


        public Task<int> GetCountSupplierAsync(string name, CancellationToken cancellationToken)
        {
            IQueryable<Dal.Supplier> queryable = generateQuery(Status.Active);

            return queryable.Where(s => s.Name.StartsWith(name)).CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<SupplierBrand>> GetManySupplierBrandAsync(Guid supplierId, CancellationToken cancellationToken)
        {
            var supplierBrands = await _context.SupplierBrands.Include(s=>s.Brand).Where(s => s.SupplierId == supplierId).ToListAsync();
            return _mapper.Map<List<SupplierBrand>>(supplierBrands);
        }

        public async Task AddOrUpdateAsync(SupplierMasterProduct supplierMasterProduct, CancellationToken cancellationToken)
        {
            var dalSupplierMasterProduct = await _context.SupplierMasterProducts.FirstOrDefaultAsync(smp => smp.SupplierId == supplierMasterProduct.SupplierId && smp.MasterProductId == supplierMasterProduct.MasterProductId);
            if (dalSupplierMasterProduct != null)
            {
                _mapper.Map(supplierMasterProduct, dalSupplierMasterProduct);

            }
            else
            {
                dalSupplierMasterProduct = _mapper.Map<Dal.SupplierMasterProduct>(supplierMasterProduct);
                _context.SupplierMasterProducts.Add(dalSupplierMasterProduct);
            }
        }
    }
}
