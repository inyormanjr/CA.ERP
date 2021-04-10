using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.Supplier;
using CA.ERP.WebApp.Blazor.Pages.Supplier;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder
{

    public class PurchaseOrderCreateViewModel : ViewModelBase
    {
        private readonly ILogger<PurchaseOrderCreateViewModel> _logger;
        private readonly IDialogService _dialogService;
        private readonly BranchService _branchService;
        private readonly SupplierService _supplierService;
        private List<BranchView> _branches = new List<BranchView>();
        private bool _branchesIsLoading = false;
        private SupplierView _supplier;
        private List<SupplierView> _suppliers = new List<SupplierView>();
        private BranchView _branch;

        public List<BranchView> Branches
        {
            get => _branches; set
            {
                _branches = value;
                OnPropertyChanged("Branches");
            }
        }

        public BranchView Branch
        {
            get => _branch; set
            {
                _branch = value;
                OnPropertyChanged("Branch");
            }
        }

        public bool BranchesIsLoading
        {
            get => _branchesIsLoading; set
            {
                _branchesIsLoading = value;
                OnPropertyChanged("BranchesIsLoading");
            }
        }

        public SupplierView Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                OnPropertyChanged("Supplier");
                _logger.LogInformation(_supplier?.Name ?? "unknown");
            }
        }

        public List<SupplierView> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged("Suppliers");
            }

        }



        public PurchaseOrderCreateViewModel(ILogger<PurchaseOrderCreateViewModel> logger, IDialogService dialogService, BranchService branchService, SupplierService supplierService)
        {
            _logger = logger;
            _dialogService = dialogService;
            _branchService = branchService;
            _supplierService = supplierService;
            Init().ConfigureAwait(false);

        }

        public async Task Init()
        {
            var loadBranchsTask = LoadBranches();


            var loadSuppliersTask = LoadSuppliers();

            await Task.WhenAll(loadBranchsTask, loadSuppliersTask);
        }

        private async Task LoadSuppliers()
        {
            //load all supplier to memory
            var paginatedSuppliers = await _supplierService.GetSuppliersAsync(null, 0, 99999);
            _logger.LogInformation($"Paginated count : {paginatedSuppliers.Data.Count().ToString()}");
            Suppliers = paginatedSuppliers.Data.ToList();
        }

        private async Task LoadBranches()
        {
            BranchesIsLoading = true;
            await _branchService.GetBranchesAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ErrorMessage = t.Exception.Message;
                }
                else
                {

                    Branches = t.Result.Data.ToList();
                    _logger.LogInformation($"Branch is loading :{BranchesIsLoading}");
                }

                BranchesIsLoading = false;
            });
        }

        public Task<IEnumerable<SupplierView>> SearchSuppliers(string name)
        {
            var suppliers = Suppliers.Where(s => s.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            _logger.LogInformation("Supplier Count {Count}", suppliers.Count);
            return Task.FromResult<IEnumerable<SupplierView>>(suppliers);
        }

        public Task<IEnumerable<BranchView>> SearchBranches(string name)
        {
            var branches = Branches.Where(s => s.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            return Task.FromResult<IEnumerable<BranchView>>(branches);
        }
    }
}
