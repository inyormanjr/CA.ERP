using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.ReportAgg
{
    public interface IBarcodeGenerator : IHelper
    {
        byte[] GenerateBarcode(string barcode);
    }
}
