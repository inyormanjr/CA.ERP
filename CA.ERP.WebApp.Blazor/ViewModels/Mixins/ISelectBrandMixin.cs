using CA.ERP.Shared.Dto.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Mixins
{
    public interface ISelectBrandMixin
    {
        public List<BrandView> Brands { get; set; }

        public BrandView SelectedBrand { get; set; }

        public async Task LoadBrands(IBrandService branchService)
        {

            var pagindatedData = await branchService.GetBranchesAsync();
            Branches = pagindatedData.Data.ToList();

        }

        public async Task<IEnumerable<BranchView>> SearchBranches(string branchName)
        {
            if (string.IsNullOrEmpty(branchName))
            {
                return Branches;
            }
            return Branches.Where(b => b.Name.Contains(branchName));
        }
    }
}
