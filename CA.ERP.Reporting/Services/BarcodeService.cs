using NetBarcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Reporting.Services
{
    public interface IBarcodeService
    {
        string GenerateBarcode(string barcode);
    }
    public class BarcodeService : IBarcodeService
    {
        public string GenerateBarcode(string barcode)
        {
            var barcodeImage = new Barcode(barcode, NetBarcode.Type.Code128, true);
            return barcodeImage.GetBase64Image();
        }
    }
}
