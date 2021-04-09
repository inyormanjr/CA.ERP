using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder
{
    public class PurchaseOrderViewModel : ViewModelBase
    {
        private readonly PurchaseOrderService _purchaseOrderService;

        public PurchaseOrderViewModel(PurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        public Task<PaginatedResponse<PurchaseOrderView>> GetPurchaseOrdersAsync(string purchaseOrderNumber, DateTimeOffset? startDate, DateTimeOffset? endDate, int page, int size)
        {
            return _purchaseOrderService.GetPurchaseOrdersAsync(purchaseOrderNumber,  startDate,  endDate, page, size);
        }
    }
}
