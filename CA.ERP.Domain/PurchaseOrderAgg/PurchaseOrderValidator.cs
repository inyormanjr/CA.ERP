using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrder>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IUserRepository _userRepository;

        public PurchaseOrderValidator(ISupplierRepository supplierRepository, IBranchRepository branchRepository, IUserRepository userRepository, IValidator<PurchaseOrderItem> purchaseOrderValidator)
        {
            _supplierRepository = supplierRepository;
            _branchRepository = branchRepository;
            _userRepository = userRepository;

            RuleFor(p => p.Barcode).NotEmpty();
            RuleFor(p => p.DeliveryDate).NotEmpty();
            RuleFor(p => p.TotalCostPrice).NotEmpty();

            RuleFor(p => p.PurchaseOrderItems).NotEmpty();

            RuleFor(t => t.SupplierId).NotEmpty().CustomAsync(SupplierExist);
            RuleFor(t => t.BranchId).NotEmpty().CustomAsync(BranchExist);
            RuleFor(p => p.ApprovedById).NotEmpty().CustomAsync(UserExist);

            RuleForEach(p => p.PurchaseOrderItems).SetValidator(purchaseOrderValidator);
        }


        private async Task SupplierExist(Guid id, CustomContext context, CancellationToken cancellationToken)
        {
            var exist = await _supplierRepository.ExistAsync(id);
            if (!exist)
            {
                context.AddFailure("Supplier should exist.");
            }
        }

        private async Task BranchExist(Guid id, CustomContext context, CancellationToken cancellationToken)
        {
            var exist = await _branchRepository.ExistAsync(id);
            if (!exist)
            {
                context.AddFailure("Branch should exist.");
            }
        }

        private async Task UserExist(Guid id, CustomContext context, CancellationToken cancellationToken)
        {
            var exist = await _userRepository.ExistAsync(id);
            if (!exist)
            {
                context.AddFailure("User should exist.");
            }
        }
    }
}
