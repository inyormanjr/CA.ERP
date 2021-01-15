using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderBarcodeGenerator : IPurchaseOrderBarcodeGenerator
    {
        private readonly IDateTimeHelper _dateTimeHelper;

        public PurchaseOrderBarcodeGenerator(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }
        public string GenerateBarcode()
        {
            var now = _dateTimeHelper.GetCurrentDateTimeOffset();
            var nowUnix = now.ToUnixTimeSeconds();
            int year = now.Year;
            var epoch = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.FromSeconds(0)).ToUnixTimeSeconds();

            var second = nowUnix - epoch;
            
            if (second == lastSecond)
            {
                second++;
            }
            var strSeconds = second.ToString("00000000");
            lastSecond = second;
            var strYear = year.ToString("0000").Substring(2, 2);
            return $"{strYear}-{strSeconds}";
        }

        static long lastSecond = -1;
    }
}
