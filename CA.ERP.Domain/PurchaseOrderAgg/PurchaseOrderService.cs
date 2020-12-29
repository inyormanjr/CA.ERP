using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderService : ServiceBase
    {
        private readonly IPurchaseOrderBarcodeGenerator _purchaseOrderBarcodeGenerator;
        private readonly IPurchaseOrderTotalAmountCalculator _purchaseOrderTotalAmountCalculator;

        public PurchaseOrderService(IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator, IPurchaseOrderTotalAmountCalculator purchaseOrderTotalAmountCalculator)
        {
            _purchaseOrderBarcodeGenerator = purchaseOrderBarcodeGenerator;
            _purchaseOrderTotalAmountCalculator = purchaseOrderTotalAmountCalculator;
        }
        public async Task CreatePurchaseOrder(DateTime deliveryDate)
        {
            var purchaseOrder = new PurchaseOrder() { Barcode = _purchaseOrderBarcodeGenerator.GenerateBarcode(), DeliveryDate = deliveryDate, };
        }
    }
}
