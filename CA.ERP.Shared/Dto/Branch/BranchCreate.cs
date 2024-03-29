using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.Branch
{
    public class BranchCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BranchNo { get; set; }
        [Required]
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
