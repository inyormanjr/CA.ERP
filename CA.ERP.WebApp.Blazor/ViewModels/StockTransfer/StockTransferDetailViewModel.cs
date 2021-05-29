using CA.ERP.Shared.Dto.StockTransfer;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockTransfer
{
    public class StockTransferDetailViewModel : ViewModelBase
    {
        private readonly ILogger<StockTransferDetailViewModel> _logger;
        private readonly IStockTransferService _stockTransferService;
        private readonly ISnackbar _snackbar;

        public int Id { get; private set; }

        public StockTransferView StockTransfer { get; private set; } = new StockTransferView();

        public StockTransferDetailViewModel(ILogger<StockTransferDetailViewModel> logger, IStockTransferService stockTransferService, ISnackbar snackbar)
        {
            _logger = logger;
            _stockTransferService = stockTransferService;
            _snackbar = snackbar;
        }

        public async Task LoadStockTransfer(Guid id)
        {
            try
            {
                StockTransfer = await _stockTransferService.GetById(id);
                OnPropertyChanged(nameof(StockTransfer));
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                _logger.LogError(ex, ex.Message);
            }
            
        }
    }
}
