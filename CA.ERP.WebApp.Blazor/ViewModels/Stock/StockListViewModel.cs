using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.Brand;
using CA.ERP.Shared.Dto.MasterProduct;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.Mixins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Stock
{
    public class StockListViewModel : ViewModelBase, ISelectBranchMixin, ISelectBrandMixin, ISelectMasterProductMixin
    {
        private readonly IStockService _stockService;
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        private readonly IMasterProductService _masterProductService;

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus? StockStatus { get; set; }

        public List<BranchView> Branches { get; set; }

        public BranchView SelectedBranch { get; set; }
        public List<BrandView> Brands { get; set; }
        public BrandView SelectedBrand { get; set; }

        public MasterProductView SelectedMasterProduct { get; set; }


        public IMasterProductService MasterProductService => _masterProductService;

        public StockListViewModel(IStockService stockService, IBranchService branchService, IBrandService brandService, IMasterProductService masterProductService)
        {
            _stockService = stockService;
            _branchService = branchService;
            _brandService = brandService;
            _masterProductService = masterProductService;
            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            var loadBranchesTask = (this as ISelectBranchMixin).LoadBranches(_branchService);

            var loadBrandsTask = (this as ISelectBrandMixin).LoadBrands(_brandService);


            await Task.WhenAll(loadBranchesTask, loadBrandsTask);

            OnPropertyChanged(nameof(Branches));
        }

        public Task<PaginatedResponse<StockView>> GetStocksAsync(int page, int size)
        {
            return _stockService.GetStocks(SelectedBranch?.Id, SelectedBrand?.Id, SelectedMasterProduct?.Id, StockNumber, SerialNumber, StockStatus, page, size);
        }
    }
}
