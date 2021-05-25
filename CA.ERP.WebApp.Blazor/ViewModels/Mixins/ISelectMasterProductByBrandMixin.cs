using CA.ERP.Shared.Dto.MasterProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Mixins
{
    public interface ISelectMasterProductByBrandMixin : ISelectBrandMixin, ISelectMasterProductMixin
    {
        public async Task<IEnumerable<MasterProductView>> SearchMasterProducts(string model)
        {
            var paginatedList = await MasterProductService.GetMasterProducts(model, SelectedBrand?.Id);
            return paginatedList.Data.ToList();
        }
    }
}
