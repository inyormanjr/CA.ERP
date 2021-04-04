using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.UpdatePurchaseOrder
{
    public class UpdatePurchaseOrderCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Guid SupplierId { get; set; }
        public Guid DesntinationBranchId { get; set; }
        public List<UpdatePurchaseOrderItem> PurchaseOrderItems { get; set; }
        public UpdatePurchaseOrderCommand(Guid id, DateTime deliveryDate, Guid supplierId, Guid desntinationBranchId, List<UpdatePurchaseOrderItem> purchaseOrderItems)
        {
            Id = id;
            DeliveryDate = deliveryDate;
            SupplierId = supplierId;
            DesntinationBranchId = desntinationBranchId;
            PurchaseOrderItems = purchaseOrderItems;
        }
    }

    public class UpdatePurchaseOrderItem
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid MasterProductId { get; set; }
        
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveredQuantity { get; set; }

        public UpdatePurchaseOrderItem(Guid id, Guid purchaseOrderId, Guid masterProductId, decimal orderedQuantity, decimal freeQuantity, decimal costPrice, decimal discount, decimal deliveredQuantity)
        {
            Id = id;
            PurchaseOrderId = purchaseOrderId;
            MasterProductId = masterProductId;
            OrderedQuantity = orderedQuantity;
            FreeQuantity = freeQuantity;
            CostPrice = costPrice;
            Discount = discount;
            DeliveredQuantity = deliveredQuantity;
        }
    }
}
