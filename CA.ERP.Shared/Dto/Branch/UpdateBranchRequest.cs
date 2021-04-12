using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.Branch
{
    /// <summary>
    /// Hold update data for updating the branch
    /// </summary>
    public class UpdateBranchRequest : UpdateBaseRequest<BranchView>
    {
    }
}
