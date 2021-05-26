using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.ErrorCodes
{
    public class StockTransferErrorCodes
    {
        public const string InvalidSourceBranch = "stock-transfer-invalid-source-branch";
        public const string InvalidDestinationBranch = "stock-transfer-invalid-destination-branch";
        public const string InvalidCreator = "stock-transfer-invalid-creator";
        public const string InvalidStockTransferId = "stock-transfer-item-invalid-stock-transfer-id";
        public const string InvalidMasterProductId = "stock-transfer-item-invalid-master-product-id";
        public const string RequestedQuantityMustGreaterThanZero = "stock-transfer-item-requested-quantity-must-be-greater-than-zero";
        public const string DuplicateItem = "stock-transfer-item-duplicate-item";
        public const string PastDeliveryDate = "stock-transfer-past-delivery-date";
    }
}
