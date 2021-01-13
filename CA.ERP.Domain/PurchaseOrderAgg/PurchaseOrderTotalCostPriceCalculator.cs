using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CA.ERP.Domain.Common.Rounding;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderTotalCostPriceCalculator : IPurchaseOrderTotalCostPriceCalculator
    {
        private readonly IRoundingCalculator _roundingCalculator;

        public PurchaseOrderTotalCostPriceCalculator(IRoundingCalculator roundingCalculator)
        {
            _roundingCalculator = roundingCalculator;
        }
        public decimal Calculate(PurchaseOrder purchaseOrder, ICollection<PurchaseOrderItem> purchaseOrderItems)
        {
            decimal unrounded = purchaseOrderItems.Select(poi => poi.TotalCostPrice).Sum();
            return _roundingCalculator.Round(unrounded);
        }
    }
}
