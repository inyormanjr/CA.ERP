using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveErrorCodes
    {
        public const string InvalidPurchaseOrderId = "stock-receive-invalid-purchase-order-id";
        public const string InvaliBranchId = "stock-receive-invalid-branch-id";
        public const string InvaliSupplierId = "stock-receive-invalid-branch-id";
        public const string UnknownStockSource = "stock-receive-unknown-stock-source";
        internal static readonly string InvaliMasterProductId;
    }
}
