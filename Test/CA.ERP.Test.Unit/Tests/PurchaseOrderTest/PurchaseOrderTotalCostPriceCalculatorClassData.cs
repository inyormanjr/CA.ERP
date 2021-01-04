using CA.ERP.Domain.PurchaseOrderAgg;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Test.Unit.Tests.PurchaseOrderTest
{
    public class PurchaseOrderTotalCostPriceCalculatorClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var purchaseOrderItems = new List<PurchaseOrderItem>();
            for (int i = 0; i < 10; i++)
            {
                var purchaseOrderItem = new PurchaseOrderItem() { TotalCostPrice = 1000 };
                purchaseOrderItems.Add(purchaseOrderItem);
            }
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            yield return new object[] { purchaseOrder, purchaseOrderItems, 1000*10 };

            purchaseOrderItems = new List<PurchaseOrderItem>();
            for (int i = 0; i < 5; i++)
            {
                var purchaseOrderItem = new PurchaseOrderItem() { TotalCostPrice = 1000 };
                purchaseOrderItems.Add(purchaseOrderItem);
            }
            purchaseOrder = new PurchaseOrder();
            yield return new object[] { purchaseOrder, purchaseOrderItems, 1000 * 5 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
