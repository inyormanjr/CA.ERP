using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierService: ServiceBase
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierFactory _supplierFactory;
        private readonly IUserHelper _userHelper;
        private readonly IValidator<Supplier> _supplerValidator;

        public SupplierService(ISupplierRepository supplierRepository, ISupplierFactory supplierFactory, IUserHelper userHelper, IValidator<Supplier> supplerValidator)
        {
            _supplierRepository = supplierRepository;
            _supplierFactory = supplierFactory;
            _userHelper = userHelper;
            _supplerValidator = supplerValidator;
        }

        public async Task<List<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken = default)
        {
            List<Supplier> suppliers = await _supplierRepository.GetAll(cancellationToken:cancellationToken);
            return suppliers;
        }

        public async Task<OneOf<Supplier, NotFound>> GetSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            OneOf<Supplier, None> supplierOption = await _supplierRepository.GetByIdAsync(supplierId, cancellationToken: cancellationToken);
            return supplierOption.Match<OneOf<Supplier, NotFound>>(
                f0: suppler => suppler,
                f1: none => default(NotFound)
                );
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateSupplierAsync(string name, string address, string contact, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            //creation
            var supplier = _supplierFactory.CreateSupplier(name, address, contact);
            supplier.CreatedBy = _userHelper.GetCurrentUserId();
            supplier.UpdatedBy = _userHelper.GetCurrentUserId();

            //validation
            var validationResult = _supplerValidator.Validate(supplier);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                ret = await _supplierRepository.AddAsync(supplier, cancellationToken: cancellationToken);
                
            }
            return ret;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateSupplierAsync(Guid supplierId, Supplier supplier, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> ret;

            //validation
            var validationResult = _supplerValidator.Validate(supplier);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                var supplierOption = await _supplierRepository.UpdateAsync(supplierId, supplier, cancellationToken: cancellationToken);
                ret = supplierOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: supplierId => supplierId,
                    f1: none => default(NotFound)
                    );

            }
            return ret;
        }

        public async Task<OneOf<Success, NotFound>> DeleteSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            OneOf<Success, NotFound> ret;

            var supplierOption = await _supplierRepository.DeleteAsync(supplierId, cancellationToken: cancellationToken);
            ret = supplierOption.Match<OneOf<Success,  NotFound>>(
                f0: supplierId => supplierId,
                f1: none => default(NotFound)
                );
            return ret;
        }

    }
}
