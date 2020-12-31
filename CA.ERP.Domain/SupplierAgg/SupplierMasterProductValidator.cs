using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierMasterProductValidator:AbstractValidator<SupplierMasterProduct>
    {
        public SupplierMasterProductValidator()
        {
            RuleFor(t => t.SupplierId).NotEmpty();
            RuleFor(t => t.MasterProductId).NotEmpty();
            RuleFor(t => t.CostPrice).GreaterThan(0);
        }
    }
}
