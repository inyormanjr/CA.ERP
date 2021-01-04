using CA.ERP.Domain.MasterProductAgg;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierMasterProductValidator : AbstractValidator<SupplierMasterProduct>
    {
        private readonly IMasterProductRepository _masterProductRepository;

        public SupplierMasterProductValidator(IMasterProductRepository masterProductRepository)
        {
            _masterProductRepository = masterProductRepository;
            RuleFor(t => t.SupplierId).NotEmpty();
            RuleFor(t => t.CostPrice).GreaterThan(0);
            RuleFor(t => t.MasterProductId).NotEmpty().CustomAsync(chekMasterProductExist);

        }

        private async Task chekMasterProductExist(Guid masterProductId, CustomContext context, CancellationToken arg3)
        {
            var exist = await _masterProductRepository.ExistAsync(masterProductId);
            if (!exist)
            {
                context.AddFailure("Master Product should exist.");
            }
        }
    }
}
