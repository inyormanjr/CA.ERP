using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    /// <summary>
    /// Business logic interface for calculating Purchase order total amount
    /// </summary>
    public interface IPurchaseOrderTotalCostPriceCalculator : IBusinessLogic
    {
        /// <summary>
        /// Do the calculation
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <param name="purchaseOrderItems"></param>
        /// <returns>The total cost base on pass purchase order and items</returns>
        public decimal Calculate(PurchaseOrder purchaseOrder, ICollection<PurchaseOrderItem> purchaseOrderItems);
    }
}
