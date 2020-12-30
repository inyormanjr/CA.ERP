using CA.ERP.Domain.Common.Rounding;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItemTotalCostPriceCalculator : IPurchaseOrderItemTotalCostPriceCalculator
    {
        private readonly IRoundingCalculator _roundingCalculator;

        public PurchaseOrderItemTotalCostPriceCalculator(IRoundingCalculator roundingCalculator)
        {
            _roundingCalculator = roundingCalculator;
        }
        public decimal Calculate(PurchaseOrderItem purchaseOrderItem)
        {
            decimal toRound =  (purchaseOrderItem.CostPrice - purchaseOrderItem.Discount) * purchaseOrderItem.OrderedQuantity;
            return _roundingCalculator.Round(toRound);
        }
    }
}
