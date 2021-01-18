using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.ReportAgg
{
    public interface IBarcodeGenerator : IHelper
    {
        string GenerateBarcode(string barcode);
    }
}
