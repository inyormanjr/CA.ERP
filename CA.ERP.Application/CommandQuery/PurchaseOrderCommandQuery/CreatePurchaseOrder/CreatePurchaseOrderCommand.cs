using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommand : IRequest<DomainResult<Guid>>
    {
        public DateTimeOffset DeliveryDate { get; set; }
        public Guid SupplierId { get; set; }
        public Guid DesntinationBranchId { get; set; }
        public List<CreatePurchaseOrderItem> PurchaseOrderItems { get; set; }

        public CreatePurchaseOrderCommand(DateTimeOffset deliveryDate, Guid supplierId, Guid desntinationBranchId, List<CreatePurchaseOrderItem> purchaseOrderItems)
        {
            DeliveryDate = deliveryDate;
            SupplierId = supplierId;
            DesntinationBranchId = desntinationBranchId;
            PurchaseOrderItems = purchaseOrderItems;
        }

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

        public CreatePurchaseOrderItem(Guid masterProductId, decimal orderedQuantity, decimal freeQuantity, decimal costPrice, decimal discount, decimal totalCostPrice, decimal deliveredQuantity)
        {
            MasterProductId = masterProductId;
            OrderedQuantity = orderedQuantity;
            FreeQuantity = freeQuantity;
            CostPrice = costPrice;
            Discount = discount;
            TotalCostPrice = totalCostPrice;
            DeliveredQuantity = deliveredQuantity;
        }
    }
}
