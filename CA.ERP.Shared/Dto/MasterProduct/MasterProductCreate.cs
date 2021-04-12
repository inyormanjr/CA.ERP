using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.MasterProduct
{
    public class MasterProductCreate
    {
        [Required]
        public string Model { get; set; }
        public string Description { get; set; }
        public ProductStatus ProductStatus { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        
    }
}
