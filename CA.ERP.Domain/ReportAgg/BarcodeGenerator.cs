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
        public byte[] GenerateBarcode(string barcode)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.CODE128, barcode, Color.Black, Color.White, 290, 120);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}
