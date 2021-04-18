using CA.ERP.Domain.Core;
using CA.ERP.Domain.PurchaseOrderAgg;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Fixtures
{
    public class PurchaseOrderFixture 
    {

        public PurchaseOrder GetPurchaseOrder(IDateTimeProvider dateTimeProvider)
        {
            

            IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator = A.Fake<IPurchaseOrderBarcodeGenerator>();

            A.CallTo(() => purchaseOrderBarcodeGenerator.GenerateBarcode()).Returns("101010");

            DateTimeOffset deliveryDate = dateTimeProvider.GetCurrentDateTimeOffset().AddDays(15);
            var purchaseOrderResult = PurchaseOrder.Create(deliveryDate, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), dateTimeProvider, purchaseOrderBarcodeGenerator);
            if (purchaseOrderResult.IsSuccess)
            {
                var purchaseOrder = purchaseOrderResult.Result;
                var createPurchaseOrderItemResult = PurchaseOrderItem.Create(purchaseOrder.Id, Guid.NewGuid(), 10, 5, 50, 10);
                if (createPurchaseOrderItemResult.IsSuccess)
                {
                    purchaseOrder.AddPurchaseOrderItem(createPurchaseOrderItemResult.Result);

                }

            }

            return purchaseOrderResult.Result;
        }
    }
}
