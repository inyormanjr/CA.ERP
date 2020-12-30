using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderFactory : IPurchaseOrderFactory
    {
        private readonly IPurchaseOrderTotalCostPriceCalculator _purchaseOrderTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalCostPriceCalculator _purchaseOrderItemTotalCostPriceCalculator;
        private readonly IPurchaseOrderBarcodeGenerator _purchaseOrderBarcodeGenerator;
        private readonly IUserHelper _userHelper;
        private readonly IPurchaseOrderItemTotalQuantityCalculator _purchaseOrderItemTotalQuantityCalculator;

        public PurchaseOrderFactory(IPurchaseOrderTotalCostPriceCalculator purchaseOrderTotalCostPriceCalculator,
            IPurchaseOrderItemTotalCostPriceCalculator purchaseOrderItemTotalCostPriceCalculator,
            IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator,
            IUserHelper userHelper,
            IPurchaseOrderItemTotalQuantityCalculator purchaseOrderItemTotalQuantityCalculator
            )
        {
            _purchaseOrderTotalCostPriceCalculator = purchaseOrderTotalCostPriceCalculator;
            _purchaseOrderItemTotalCostPriceCalculator = purchaseOrderItemTotalCostPriceCalculator;
            _purchaseOrderBarcodeGenerator = purchaseOrderBarcodeGenerator;
            _userHelper = userHelper;
            _purchaseOrderItemTotalQuantityCalculator = purchaseOrderItemTotalQuantityCalculator;
        }

        public PurchaseOrder Create(DateTime deliveryDate, Guid supplierId, Guid branchId, List<PurchaseOrderItem> purchaseOrderItems)
        {
            var purchaseOrder = new PurchaseOrder() { Barcode = _purchaseOrderBarcodeGenerator.GenerateBarcode(), DeliveryDate = deliveryDate, ApprovedById = _userHelper.GetCurrentUserId(), SupplierId = supplierId, BranchId = branchId };
            //do computations here
            foreach (var purchaseOrderItem in purchaseOrderItems)
            {
                purchaseOrderItem.TotalCostPrice = _purchaseOrderItemTotalCostPriceCalculator.Calculate(purchaseOrderItem);
                purchaseOrderItem.TotalQuantity = _purchaseOrderItemTotalQuantityCalculator.Calculate(purchaseOrderItem);
            }
            purchaseOrder.PurchaseOrderItems = purchaseOrderItems;
            purchaseOrder.TotalCostPrice = _purchaseOrderTotalCostPriceCalculator.Calculate(purchaseOrder, purchaseOrderItems);
            return purchaseOrder;
        }
    }
}
