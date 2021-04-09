using System;
using System.ComponentModel.DataAnnotations;

namespace CA.ERP.Shared.Dto.Branch
{
    public class BranchView: DtoViewBase
    {        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
