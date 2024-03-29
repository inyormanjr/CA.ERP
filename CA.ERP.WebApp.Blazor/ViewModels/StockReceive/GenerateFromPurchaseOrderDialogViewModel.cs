using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Input;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockReceive
{
    public class GenerateFromPurchaseOrderDialogViewModel : ViewModelBase
    {
        private readonly IBranchService _branchService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IStockReceiveService _stockReceiveService;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;
        private readonly ILogger<GenerateFromPurchaseOrderDialogViewModel> _logger;
        private BranchView _selectedBranch;
        private PurchaseOrderView _selectedPurchaseOrder;
        private bool _isSaving;

        public ICommand GenerateAsyncCommand { get; private set; }

        public List<BranchView> Branches { get; set; } = new List<BranchView>();
        public BranchView SelectedBranch
        {
            get => _selectedBranch; set
            {
                _selectedBranch = value;
                SelectedPurchaseOrder = null;
                OnPropertyChanged(nameof(SelectedBranch));
            }
        }
        public PurchaseOrderView SelectedPurchaseOrder
        {
            get => _selectedPurchaseOrder; set
            {
                _selectedPurchaseOrder = value;
                OnPropertyChanged(nameof(SelectedPurchaseOrder));
            }
        }

        public bool IsSaving
        {
            get => _isSaving; set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
            }
        }

        public GenerateFromPurchaseOrderDialogViewModel(IBranchService branchService, IPurchaseOrderService purchaseOrderService, IStockReceiveService stockReceiveService, NavigationManager navigationManager, ISnackbar snackbar, ILogger<GenerateFromPurchaseOrderDialogViewModel> logger)
        {
            _branchService = branchService;
            _purchaseOrderService = purchaseOrderService;
            _stockReceiveService = stockReceiveService;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
            _logger = logger;

            GenerateAsyncCommand = new AsyncRelayCommand(generateAsync, canGenerate);

            LoadBranches().ConfigureAwait(false);
        }

        public async Task LoadBranches()
        {
            var paginatedData = await _branchService.GetBranchesAsync();
            Branches = paginatedData.Data.ToList();
            OnPropertyChanged(nameof(Branches));
        }

        public Task<IEnumerable<BranchView>> SearchBranches(string name)
        {
            var branches = Branches.Where(s => s.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            return Task.FromResult<IEnumerable<BranchView>>(branches);
        }

        public async Task<IEnumerable<PurchaseOrderView>> SearchPurchaseOrders(string purchaseOrderNumber)
        {
            var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersAsync(SelectedBranch?.Id, purchaseOrderNumber, null, null, PurchaseOrderStatus.Pending, 0, 10);
            return purchaseOrders.Data;
        }


        private bool canGenerate()
        {

            var canExecute = !IsSaving && SelectedPurchaseOrder != null && SelectedBranch != null && SelectedPurchaseOrder.DestinationBranchId == SelectedBranch.Id;
            _logger.LogInformation("Can Execute {canExecute}, Purchase Order {SelectedPurchaseOrder}, Branch {SelectedBranch},", canExecute, SelectedPurchaseOrder?.DestinationBranchId, SelectedBranch?.Id);
            return canExecute;
        }

        private async Task generateAsync()
        {
            if (SelectedPurchaseOrder != null)
            {
                try
                {
                    IsSaving = true;
                    var stockReceiveId = await _stockReceiveService.GenerateStockReceiveFromPurchaseOrderAsync(SelectedPurchaseOrder);

                    _navigationManager.NavigateTo($"/stock-receive/edit/{stockReceiveId}");
                }
                catch (ValidationException ex)
                {
                    _snackbar.Add(ex.Message);
                    Errors = ex.ValidationErrors.SelectMany(ve => ve.Value).ToArray();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error generating stock receive from purchase order", ex);
                    _snackbar.Add(ex.Message, Severity.Error);
                }
                finally
                {
                    IsSaving = false;
                }

            }

        }
    }
}
