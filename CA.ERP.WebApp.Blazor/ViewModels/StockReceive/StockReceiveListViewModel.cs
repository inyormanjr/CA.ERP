using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.Shared.Dto.Supplier;
using CA.ERP.WebApp.Blazor.Services;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockReceive
{
    public class StockReceiveListViewModel : ViewModelBase
    {
        private readonly IStockReceiveService _stockReceiveService;
        private readonly ISnackbar _snackbar;

        public StockReceiveListViewModel(IStockReceiveService stockReceiveService, ISnackbar snackbar)
        {
            _stockReceiveService = stockReceiveService;
            _snackbar = snackbar;
        }

        public BranchView SelectedBranch { get;  set; }
        public SupplierView SelectedSupplier { get;  set; }
        public DateTimeOffset? DateReceive { get;  set; }

        public Task<PaginatedResponse<StockReceiveView>> SearchStockReceivesAsync(int page, int size) {
            var ret = new PaginatedResponse<StockReceiveView>();
            try
            {
                return _stockReceiveService.GetStockReceivesAsync(SelectedBranch?.Id, SelectedSupplier?.Id, DateReceive, page, size);
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                ErrorMessage = ex.Message;

            }

            return Task.FromResult(ret);
        }
    }
}
