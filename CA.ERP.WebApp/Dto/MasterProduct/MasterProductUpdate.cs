using CA.ERP.Common.Types;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.MasterProduct
{
    public class MasterProductUpdate
    {
        [Required]
        public string Model { get; set; }
        public string Description { get; set; }
        public ProductStatus ProductStatus { get; set; }
        [Required]
        public Guid BrandId { get; set; }
    }
}
