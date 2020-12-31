using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderBarcodeGenerator : IPurchaseOrderBarcodeGenerator
    {
        public string GenerateBarcode()
        {
            var epoch = new DateTimeOffset(2020, 12, 1, 0, 0, 0, TimeSpan.FromSeconds(0)).ToUnixTimeSeconds();
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            return (now - epoch).ToString("00000000000");
        }
    }
}
