using CA.ERP.WebApp.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class CreateBranchRequest
    {
        [Required]
        public Branch Branch { get; set; }
    }
}
