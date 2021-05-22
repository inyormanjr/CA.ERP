using CA.ERP.Shared.Dto.MasterProduct;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Mixins
{
    public interface ISelectMasterProductMixin
    {
        public MasterProductView SelectedMasterProduct { get; set; }

        public IMasterProductService MasterProductService { get; }


        public async Task<IEnumerable<MasterProductView>> SearchMasterProducts(string model)
        {
            var paginatedList = await MasterProductService.GetMasterProducts(model);
            return paginatedList.Data.ToList();
        }
    }
}
