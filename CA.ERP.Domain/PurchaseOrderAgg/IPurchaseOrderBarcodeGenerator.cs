using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderBarcodeGenerator : IHelper
    {
        string GenerateBarcode();
    }
}
