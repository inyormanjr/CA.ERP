using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderErrorCodes
    {

        public const string DeliveryDatePast = "purchase-order-delivery-date-past";
        public const string EmptyPurchaseOrderItems = "purchase-order-purchase-order-item-empty";
        public const string TotalQuantityLessThanZero = "purchase-order-total-quantity-less-than-zero";
        public const string IdentityNotFound = "purchase-order-identity-not-found";
        public const string NotFound = "purchase-order-not-found";
        public const string DenyOtherUser = "purchase-order-deny-other-users";

    }
}
