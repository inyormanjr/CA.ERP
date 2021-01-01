using CA.ERP.Domain.Base;
using CA.ERP.Domain.BrandAgg;
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
        private readonly IValidator<Supplier> _supplerValidator;
        private readonly ISupplierMasterProductRepository _supplierMasterProductRepository;
        private readonly IValidator<SupplierMasterProduct> _supplierMasterProductValidator;
        private readonly IBrandRepository _brandRepository;
        private readonly IValidator<SupplierBrand> _supplierBrandValidator;

        public SupplierService(
            ISupplierRepository supplierRepository, ISupplierFactory supplierFactory, IUserHelper userHelper,
            IValidator<Supplier> supplerValidator, ISupplierMasterProductRepository supplierMasterProductRepository,
            IValidator<SupplierMasterProduct> supplierMasterProductValidator, IBrandRepository brandRepository,
            IValidator<SupplierBrand> supplierBrandValidator)
            :base(supplierRepository, supplerValidator, userHelper)
        {
            _supplierRepository = supplierRepository;
            _supplierFactory = supplierFactory;
            _supplerValidator = supplerValidator;
            _supplierMasterProductRepository = supplierMasterProductRepository;
            _supplierMasterProductValidator = supplierMasterProductValidator;
            _brandRepository = brandRepository;
            _supplierBrandValidator = supplierBrandValidator;
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

        public async Task<OneOf<Success, List<ValidationFailure>>> AddOrUpdateSupplierMasterProductAsync(Guid supplierId, Guid masterProductId, decimal costPrice, CancellationToken cancellationToken)
        {
            OneOf<Success, List<ValidationFailure>> ret;
            SupplierMasterProduct supplierMasterProduct = new SupplierMasterProduct() { SupplierId = supplierId, MasterProductId = masterProductId, CostPrice = costPrice };
            var validationResult = await _supplierMasterProductValidator.ValidateAsync(supplierMasterProduct);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                supplierMasterProduct.CreatedBy = _userHelper.GetCurrentUserId();
                supplierMasterProduct.UpdatedBy = _userHelper.GetCurrentUserId();
                await _supplierMasterProductRepository.AddOrUpdateAsync(supplierMasterProduct, cancellationToken);
                ret = default(Success);
            }
            return ret;
        }

        public async Task<OneOf<Success, List<ValidationFailure>, NotFound>> AddSupplierBrand(Guid supplierId, Guid brandId, CancellationToken cancellationToken)
        {
            OneOf<Success, List<ValidationFailure>, NotFound> ret;
            SupplierBrand supplierBrand = new SupplierBrand() {
                BrandId = brandId,
                SupplierId = supplierId
            };

            var validationResult = _supplierBrandValidator.Validate(supplierBrand);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                supplierBrand.CreatedBy = _userHelper.GetCurrentUserId();
                supplierBrand.UpdatedBy = _userHelper.GetCurrentUserId();
                var option = await _supplierRepository.AddSupplierBrandAsync(supplierId, supplierBrand, cancellationToken);
                ret = option.Match<OneOf<Success, List<ValidationFailure>, NotFound>>(
                    f0: success => success, 
                    f1: none => default(NotFound)
                );
            }
            return ret;
        }

        public async Task<OneOf<Success, NotFound>> DeleteSupplierBrandAsync(Guid id, Guid brandId, CancellationToken cancellationToken)
        {
            var option = await _supplierRepository.DeleteSupplierBrandAsync(id, brandId, cancellationToken);
            return option.Match<OneOf<Success, NotFound>>(
                f0: success => success,
                f1: none => default(NotFound)
            );
        }

        public async Task<List<SupplierBrandLite>> GetSupplierBrandsAsync(Guid supplierId, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSupplierBrandsAsync(supplierId, cancellationToken:cancellationToken);
        }

    }
}
