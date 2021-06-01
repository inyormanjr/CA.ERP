using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto.StockTransfer;
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
    public class GenerateFromStockTransferDialogViewModel : ViewModelBase
    {
        private readonly IStockTransferService _stockTransferService;
        private readonly IStockReceiveService _stockReceiveService;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;
        private readonly ILogger<GenerateFromPurchaseOrderDialogViewModel> _logger;

        public StockTransferView SelectedStockTransfer { get; set; }

        public ICommand GenerateAsyncCommand { get; private set; }


        public bool IsSaving { get; private set; }

        public GenerateFromStockTransferDialogViewModel(IStockTransferService stockTransferService, IStockReceiveService stockReceiveService, NavigationManager navigationManager, ISnackbar snackbar, ILogger<GenerateFromPurchaseOrderDialogViewModel> logger)
        {
            _stockTransferService = stockTransferService;
            _stockReceiveService = stockReceiveService;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
            _logger = logger;

            GenerateAsyncCommand = new AsyncRelayCommand(generateAsync, canGenerate);
        }

        public async Task<IEnumerable<StockTransferView>> Search(string number)
        {
            var paginatedResponse = await _stockTransferService.GetStockTransfersAsync(number: number,stockTransferStatus: StockTransferStatus.Pending);
            return paginatedResponse.Data.ToList();
        }

        private async Task generateAsync()
        {
            if (SelectedStockTransfer != null)
            {
                try
                {
                    IsSaving = true;
                    var stockReceiveId = await _stockReceiveService.GenerateStockReceiveFromStockTransferAsyncAsync(SelectedStockTransfer);

                    _navigationManager.NavigateTo($"/stock-receive/{stockReceiveId}");
                }
                catch (ValidationException ex)
                {
                    _snackbar.Add(ex.Message);
                    Errors = ex.ValidationErrors.SelectMany(ve => ve.Value).ToArray();
                    OnPropertyChanged(nameof(Errors));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error generating stock receive from stock transfer", ex);
                    _snackbar.Add(ex.Message, Severity.Error);
                }
                finally
                {
                    IsSaving = false;
                    OnPropertyChanged(nameof(IsSaving));
                }

            }

        }

        private bool canGenerate()
        {

            return !IsSaving && SelectedStockTransfer != null;
        }
    }
}
