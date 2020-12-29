using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrder>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(p => p.Barcode).NotEmpty();
            RuleFor(p => p.DeliveryDate).NotEmpty();
            RuleFor(p => p.TotalCostPrice).NotEmpty();
            RuleFor(p => p.ApprovedById).NotEmpty();
            RuleFor(p => p.SupplierId).NotEmpty();
            RuleFor(p => p.BranchId).NotEmpty();
            RuleFor(p => p.PurchaseOrderItems).NotEmpty();

            RuleForEach(p => p.PurchaseOrderItems).SetValidator(new PurchaseOrderItemValidator());
        }
    }
}
