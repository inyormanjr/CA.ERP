using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Brand
{
    public class BrandCreate
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
