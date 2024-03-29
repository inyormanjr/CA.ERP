using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.ErrorCodes
{
    public class StockReceiveErrorCodes
    {
        public const string InvalidPurchaseOrderId = "stock-receive-invalid-purchase-order-id";
        public const string InvaliBranchId = "stock-receive-invalid-branch-id";
        public const string InvaliSupplierId = "stock-receive-invalid-branch-id";
        public const string UnknownStockSource = "stock-receive-unknown-stock-source";
        public const string InvalidPurchaseOrderItemId = "stock-receive-invalid-purchase-order-item-id";
        public const string EmptyStockNumber = "stock-receive-empty-stock-number";
        public const string EmptySerialNumber = "stock-receive-empty-serial-number";
        public const string InvaliMasterProductId = "stock-receive-invalid-master-product";
        public const string NotFound = "stock-receive-not-found";
        public const string CostPriceLessThanZero = "stock-receive-item-cost-price-less-than-zero";
        public const string AlreadyCommitted = "stock-receive-already-committed";
        public const string PurchaseOrderNotPending = "stock-receive-generation-purchase-order-not-pending";
        public const string InvalidStockSource = "stock-receive-invalid-stock-source";
    }
}
