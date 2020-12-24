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

namespace CA.ERP.DataAccess.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            var dalSupplier = _mapper.Map<Dal.Supplier>(supplier);
            await _context.AddAsync(dalSupplier);
            await _context.SaveChangesAsync();
            return dalSupplier.Id;
        }



        public async Task<OneOf<Supplier, None>> GetByIdAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            OneOf<Supplier, None> ret = null;

            var dalSupplier = await _context.Suppliers.Include(s => s.SupllierBrands).FirstOrDefaultAsync(s => s.Id == supplierId);

            if (dalSupplier != null)
            {
                ret = _mapper.Map<Supplier>(dalSupplier);
            }
            return ret;
        }


        public async Task<List<Supplier>> GetAll(int skip = 0, int take = 0, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            Common.Status dalStatus = (Common.Status)(int)status;
            var dalSuppliers = await _context.Suppliers.Include(s => s.SupllierBrands).Where(s => s.Status == dalStatus).ToListAsync();
            return _mapper.Map<List<Supplier>>(dalSuppliers);
        }


        public async Task<OneOf<Guid, None>> UpdateAsync(Guid supplierId, Supplier supplier, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> ret = default(None);
            var dalSupplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == supplierId, cancellationToken: cancellationToken);
            if (dalSupplier != null)
            {
                _mapper.Map<Supplier, Dal.Supplier>(supplier, dalSupplier);
                dalSupplier.Id = supplierId;
                await _context.SaveChangesAsync();
                ret = dalSupplier.Id;
            }
            
            return ret;
        }

        public async Task<OneOf<Success, None>> DeleteAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            OneOf<Success, None> ret = default(None);

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == supplierId, cancellationToken: cancellationToken);
            if (supplier != null)
            {
                _context.Entry(supplier).State = EntityState.Deleted;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                ret = default(Success);
            }
            return ret;
        }

    }
}
