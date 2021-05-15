using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockReceive
{
    public class StockReceiveDetailViewModel : ViewModelBase
    {
        private readonly IStockReceiveService _stockReceiveService;
        private readonly ISnackbar _snackbar;
        private readonly ILogger<StockReceiveEditViewModel> _logger;
        private readonly NavigationManager _navigationManager;

        public StockReceiveView StockReceive { get; set; }

        public bool IsSaving { get; set; }

        public StockReceiveDetailViewModel(IStockReceiveService stockReceiveService, ISnackbar snackbar, ILogger<StockReceiveEditViewModel> logger, NavigationManager navigationManager)
        {
            _stockReceiveService = stockReceiveService;
            _snackbar = snackbar;
            _logger = logger;
            _navigationManager = navigationManager;
        }

        public async Task PopulateStockReceive(Guid id)
        {
            StockReceive = await _stockReceiveService.GetStockReceiveByIdWithItems(id);
            OnPropertyChanged(nameof(StockReceive));
        }

        public bool CanBeFilledUp()
        {
            return StockReceive?.StockSource == StockSource.PurchaseOrder && StockReceive?.Stage == StockReceiveStage.Pending;
        }

        public bool CanBeApproved()
        {
            return StockReceive?.StockSource == StockSource.Direct && StockReceive?.Stage == StockReceiveStage.Pending;
        }

    }
}
