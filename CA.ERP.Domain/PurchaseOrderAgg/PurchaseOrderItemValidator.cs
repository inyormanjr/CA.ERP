using CA.ERP.Domain.MasterProductAgg;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItemValidator : AbstractValidator<PurchaseOrderItem>
    {
        private readonly IMasterProductRepository _masterProductRepository;

        public PurchaseOrderItemValidator(IMasterProductRepository masterProductRepository)
        {
            _masterProductRepository = masterProductRepository;


            
            RuleFor(poi => poi.TotalQuantity).GreaterThanOrEqualTo(0);
            RuleFor(poi => poi.TotalCostPrice).GreaterThanOrEqualTo(0);
            RuleFor(poi => poi.MasterProductId).NotEmpty()
                .CustomAsync(MasterProductExist);

        }

        private async Task MasterProductExist(Guid id, CustomContext context, CancellationToken cancellationToken)
        {
            var exist = await _masterProductRepository.ExistAsync(id);
            if (!exist)
            {
                context.AddFailure("Master product should exist.");
            }
        }
    }
}
