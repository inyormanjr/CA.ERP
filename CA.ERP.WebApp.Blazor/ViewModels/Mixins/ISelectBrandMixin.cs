using CA.ERP.Shared.Dto.Brand;
using CA.ERP.WebApp.Blazor.Services;
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

        public async Task LoadBrands(IBrandService brandService)
        {

            var pagindatedData = await brandService.GetBrandsAsync();
            Brands = pagindatedData.Data.ToList();

        }

        public async Task<IEnumerable<BrandView>> SearchBrands(string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
            {
                return Brands;
            }
            return Brands.Where(b => b.Name.Contains(brandName));
        }
    }
}
