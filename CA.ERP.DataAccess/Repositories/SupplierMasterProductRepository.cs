using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
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
    public class SupplierMasterProductRepository : AbstractRepository<SupplierMasterProduct, Dal.SupplierMasterProduct>, ISupplierMasterProductRepository
    {
        public SupplierMasterProductRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task AddOrUpdateAsync(SupplierMasterProduct supplierMasterProduct, CancellationToken cancellationToken)
        {
            var dalSupplierMasterProduct = await _context.SupplierMasterProducts.FirstOrDefaultAsync(smp => smp.SupplierId == supplierMasterProduct.SupplierId && smp.MasterProductId == supplierMasterProduct.MasterProductId);
            if (dalSupplierMasterProduct != null)
            {
                _mapper.Map(supplierMasterProduct, dalSupplierMasterProduct);

                //id should never be modified
                _context.Entry(dalSupplierMasterProduct).Property(t => t.Id).IsModified = false;

            }
            else
            {
                dalSupplierMasterProduct = _mapper.Map<Dal.SupplierMasterProduct>(supplierMasterProduct);
                _context.SupplierMasterProducts.Add(dalSupplierMasterProduct);
            }
            await _context.SaveChangesAsync();
        }
    }
}
