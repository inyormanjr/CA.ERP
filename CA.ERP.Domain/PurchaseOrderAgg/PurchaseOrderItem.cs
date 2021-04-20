using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItem : IEntity
    {
        public Guid Id { get; private set; }
        public Status Status { get; private set; }
        public Guid PurchaseOrderId { get; private set; }
        public Guid MasterProductId { get; private set; }
        public decimal OrderedQuantity { get; private set; }
        public decimal FreeQuantity { get; private set; }
        public decimal TotalQuantity
        {
            get
            {
                return OrderedQuantity + FreeQuantity;
            }
        }
        public decimal CostPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalCostPrice
        {
            get
            {
                return (CostPrice - Discount) * OrderedQuantity;
            }
        }
        public decimal DeliveredQuantity { get; private set; }
        public PurchaseOrderItemStatus PurchaseOrderItemStatus { get; private set; }

        public string BrandName { get; private set; }
        public string Model { get; private set; }

        private PurchaseOrderItem(Guid id, Status status, Guid purchaseOrderId, Guid masterProductId, decimal orderedQuantity, decimal freeQuantity, decimal costPrice, decimal discount)
        {
            Id = id;
            Status = status;
            PurchaseOrderId = purchaseOrderId;
            MasterProductId = masterProductId;
            OrderedQuantity = orderedQuantity;
            FreeQuantity = freeQuantity;
            CostPrice = costPrice;
            Discount = discount;
        }


        public static DomainResult<PurchaseOrderItem> Create(Guid purchaseOrderId, Guid masterProductId, decimal orderedQuantity, decimal freeQuantity, decimal costPrice, decimal discount)
        {
            if (orderedQuantity + freeQuantity <= 0)
            {
                return DomainResult<PurchaseOrderItem>.Error(PurchaseOrderErrorCodes.TotalQuantityLessThanZero, "Total quantity should be greater than zero.");
            }
            var purchaseOrderItem = new PurchaseOrderItem(Guid.NewGuid(), Status.Active, purchaseOrderId, masterProductId, orderedQuantity, freeQuantity, costPrice, discount);
            return DomainResult<PurchaseOrderItem>.Success(purchaseOrderItem);
        }


    }
}
