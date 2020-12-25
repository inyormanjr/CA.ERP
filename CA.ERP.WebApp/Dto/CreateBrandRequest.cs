using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class CreateBrandRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
