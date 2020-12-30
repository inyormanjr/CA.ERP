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
            RuleFor(poi => poi.MasterProductId).NotEmpty();
            RuleFor(poi => poi.TotalQuantity).NotEmpty();
            RuleFor(poi => poi.TotalCostPrice).NotEmpty();
        }
    }
}
