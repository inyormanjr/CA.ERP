using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.Brand;
using CA.ERP.Shared.Dto.MasterProduct;
using CA.ERP.Shared.Dto.StockTransfer;
using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.Mixins;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockTransfer
{
    public class StockTransferCreateViewModel : ViewModelBase, ISelectBranchMixin, ISelectMasterProductByBrandMixin
    {
        private readonly ILogger<StockTransferCreateViewModel> _logger;
        private readonly IMasterProductService _masterProductService;
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        private BrandView _selectedBrand;
        private MasterProductView _selectedMasterProduct;

        public List<BranchView> Branches { get; set; }

        public BranchView SourceBranch { get; set; }

        public BranchView DestinationBranch { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public StockTransferCreate StockTransfer { get; set; } = new StockTransferCreate();

        public MasterProductView SelectedMasterProduct
        {
            get => _selectedMasterProduct; set
            {
                _selectedMasterProduct = value;
                OnPropertyChanged(nameof(SelectedMasterProduct));
            }
        }

        public IMasterProductService MasterProductService => _masterProductService;

        public List<BrandView> Brands { get; set; }

        public BrandView SelectedBrand
        {
            get => _selectedBrand; set
            {
                _selectedBrand = value;
                OnPropertyChanged(nameof(SelectedBrand));
            }
        }

        public StockTransferCreateViewModel(ILogger<StockTransferCreateViewModel> logger, IMasterProductService masterProductService, IBranchService branchService, IBrandService brandService)
        {
            _logger = logger;
            _masterProductService = masterProductService;
            _branchService = branchService;
            _brandService = brandService;

            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            await Task.WhenAll(
                (this as ISelectBranchMixin).LoadBranches(_branchService),
                (this as ISelectMasterProductByBrandMixin).LoadBrands(_brandService)
                );

            OnPropertyChanged(nameof(Branches));
        }

        public void AddItemExecute()
        {
            _logger.LogInformation("Add");

            var stockTransferItem = new StockTransferItemCreate()
            {
                BrandName = SelectedBrand?.Name,
                Model = SelectedMasterProduct?.Model,
                MasterProductId = SelectedMasterProduct.Id,
                RequestedQuantity = 0
            };

            StockTransfer.Items.Add(stockTransferItem);

            OnPropertyChanged(nameof(StockTransfer));
        }

        public bool AddItemCanExecute()
        {
            return SelectedMasterProduct != null && StockTransfer.Items.Any(i => i.MasterProductId == SelectedMasterProduct.Id);
        }

        public void RemoveItem(StockTransferItemCreate item)
        {
            StockTransfer?.Items?.Remove(item);
            OnPropertyChanged(nameof(StockTransfer));
        }

        public async Task SaveAsync()
        {

        }
    }
}
