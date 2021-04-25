using CA.ERP.Shared.Dto.Branch;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Mixins
{
    public interface ISelectBranchMixin
    {

        public List<BranchView> Branches { get; set; }
        public BranchView SelectedBranch { get; set; }
        public async Task LoadBranches(IBranchService branchService)
        {

            var pagindatedData = await branchService.GetBranchesAsync();
            Branches = pagindatedData.Data.ToList();

        }
    }
}
