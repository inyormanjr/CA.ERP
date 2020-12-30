using System;
using System.ComponentModel.DataAnnotations;

namespace CA.ERP.WebApp.Dto
{
    public class Branch: DtoViewBase
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}