using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommand : IRequest<DomainResult<Guid>>
    {
        public DateTime DeliveryDate { get; set; }
        public Guid SupplierId { get; set; }
        public Guid DesntinationBranchId { get; set; }
        public List<CreatePurchaseOrderItem> PurchaseOrderItems { get; set; }
        
    }

    public class CreatePurchaseOrderItem
    {
        public Guid MasterProductId { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal DeliveredQuantity { get; set; }
    }
}
