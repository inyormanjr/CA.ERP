using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.Brand
{
    public class BrandCreate
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
