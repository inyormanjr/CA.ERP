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
    public class SupplierService: ServiceBase<Supplier>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierFactory _supplierFactory;
        private readonly IUserHelper _userHelper;
        private readonly IValidator<Supplier> _supplerValidator;

        public SupplierService(ISupplierRepository supplierRepository, ISupplierFactory supplierFactory, IUserHelper userHelper, IValidator<Supplier> supplerValidator)
            :base(supplierRepository, supplerValidator)
        {
            _supplierRepository = supplierRepository;
            _supplierFactory = supplierFactory;
            _userHelper = userHelper;
            _supplerValidator = supplerValidator;
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

        
    }
}
