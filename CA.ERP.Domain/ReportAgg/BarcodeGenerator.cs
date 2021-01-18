using NetBarcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace CA.ERP.Domain.ReportAgg
{
    public class BarcodeGenerator : IBarcodeGenerator
    {
        public string GenerateBarcode(string barcode)
        {
            var barcodeImage = new Barcode(barcode, NetBarcode.Type.Code128, true);
            return barcodeImage.GetBase64Image();
        }
    }
}
