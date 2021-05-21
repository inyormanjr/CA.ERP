using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Stock
{
    public class StockListViewModel : ViewModelBase
    {
        private readonly IStockService _stockService;

        public Guid? BranchId { get; set; }

        public Guid? BrandId { get; set; }

        public Guid? MasterProductId { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus? StockStatus { get; set; }

        public StockListViewModel(IStockService stockService)
        {
            _stockService = stockService;
        }
        public Task<PaginatedResponse<StockView>> GetStocksAsync(int page, int size)
        {
            return _stockService.GetStocks(BranchId, BrandId, MasterProductId, StockNumber, SerialNumber, StockStatus, page, size);
        }
    }
}
