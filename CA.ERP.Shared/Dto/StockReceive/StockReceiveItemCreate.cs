using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveItemCreate
    {
        [Required]
        public Guid MasterProductId { get; set; }

        public string SerialNumber { get; set; }
        public decimal CostPrice { get; set; }

        public string BrandName { get; set; }
        public string Model { get; set; }
    }
}
