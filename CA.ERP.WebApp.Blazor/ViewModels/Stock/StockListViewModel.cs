using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.Mixins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Stock
{
    public class StockListViewModel : ViewModelBase, ISelectBranchMixin
    {
        private readonly IStockService _stockService;
        private readonly IBranchService _branchService;

        public Guid? BrandId { get; set; }

        public Guid? MasterProductId { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus? StockStatus { get; set; }

        public List<BranchView> Branches { get; set; }

        public BranchView SelectedBranch { get; set; }


        public StockListViewModel(IStockService stockService, IBranchService branchService)
        {
            _stockService = stockService;
            _branchService = branchService;

            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            var loadBranchTask = (this as ISelectBranchMixin).LoadBranches(_branchService);

            await Task.WhenAll(loadBranchTask);
        }

        public Task<PaginatedResponse<StockView>> GetStocksAsync(int page, int size)
        {
            return _stockService.GetStocks(SelectedBranch?.Id, BrandId, MasterProductId, StockNumber, SerialNumber, StockStatus, page, size);
        }
    }
}
