using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItemValidator : AbstractValidator<PurchaseOrderItem>
    {
        public PurchaseOrderItemValidator()
        {
            RuleFor(poi => poi.PurchaseOrderId).NotEmpty();
            RuleFor(poi => poi.MasterProductId).NotEmpty();
        }
    }
}
