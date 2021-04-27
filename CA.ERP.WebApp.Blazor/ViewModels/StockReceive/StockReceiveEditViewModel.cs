using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockReceive
{
    public class StockReceiveEditViewModel : ViewModelBase
    {
        private readonly IStockReceiveService _stockReceiveService;
        private readonly ISnackbar _snackbar;
        private readonly ILogger<StockReceiveEditViewModel> _logger;
        private readonly NavigationManager _navigationManager;

        public StockReceiveCommit StockReceive { get;  set; }
        public bool IsSaving { get; set; }

        public StockReceiveEditViewModel(IStockReceiveService stockReceiveService, ISnackbar snackbar, ILogger<StockReceiveEditViewModel> logger, NavigationManager navigationManager)
        {
            _stockReceiveService = stockReceiveService;
            _snackbar = snackbar;
            _logger = logger;
            _navigationManager = navigationManager;
        }

        public async Task PopulateStockReceive(Guid id)
        {
            var view = await _stockReceiveService.GetStockReceiveByIdWithItems(id);
            StockReceive = _stockReceiveService.ConvertStockReceiveViewToCommit(view);
            OnPropertyChanged(nameof(StockReceive));
        }

        public async Task Submit()
        {
            try
            {
                IsSaving = true;
                OnPropertyChanged("IsSaving");
                await _stockReceiveService.Commit(StockReceive);
                _navigationManager.NavigateTo("/stock-receive/");
            }
            catch (ValidationException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                Errors = ex.ValidationErrors.SelectMany(ve => ve.Value).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving purchase order", ex);
                _snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsSaving = false;
                OnPropertyChanged("IsSaving");
            }
        }
    }
}
