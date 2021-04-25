using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder
{
    public class PurchaseOrderListViewModel : ViewModelBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderListViewModel(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        public Task<PaginatedResponse<PurchaseOrderView>> GetPurchaseOrdersAsync(string purchaseOrderNumber, DateTimeOffset? startDate, DateTimeOffset? endDate, int page, int size)
        {
            return _purchaseOrderService.GetPurchaseOrdersAsync(null, purchaseOrderNumber,  startDate,  endDate, null, page, size);
        }
    }
}
