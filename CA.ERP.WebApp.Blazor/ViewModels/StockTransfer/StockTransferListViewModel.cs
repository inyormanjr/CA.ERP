using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockTransfer;
using CA.ERP.WebApp.Blazor.Services;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockTransfer
{
    public class StockTransferListViewModel : ViewModelBase
    {
        private readonly IStockTransferService _stockTransferService;
        private readonly ISnackbar _snackbar;

        public StockTransferListViewModel(IStockTransferService stockTransferService, ISnackbar snackbar)
        {
            _stockTransferService = stockTransferService;
            _snackbar = snackbar;
        }

        public Task<PaginatedResponse<StockTransferView>> SearchStockTransferAsync(int page, int size)
        {

            try
            {
                return _stockTransferService.GetStockTransfersAsync(page, size);
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                ErrorMessage = ex.Message;

            }

            return Task.FromResult(new PaginatedResponse<StockTransferView>());
        }
    }
}
