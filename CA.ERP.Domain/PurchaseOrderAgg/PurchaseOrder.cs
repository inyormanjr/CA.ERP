using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrder : IEntity
    {

        public Guid Id { get; private set; }

        public Status Status { get; private set; }
        public string Barcode { get; private set; }
        public DateTimeOffset DeliveryDate { get; private set; }

        public Guid OrderedById { get; private set; }
        public Guid SupplierId { get; private set; }
        public Guid BranchId { get; private set; }

        public decimal TotalFreeQuantity
        {
            get
            {
                return PurchaseOrderItems.Select(poi => poi.FreeQuantity).Sum();
            }
        }

        public decimal TotalOrderedQuantity
        {
            get
            {
                return PurchaseOrderItems.Select(poi => poi.OrderedQuantity).Sum();
            }
        }

        public decimal TotalCostPrice
        {
            get
            {
                return PurchaseOrderItems.Select(poi => poi.TotalCostPrice).DefaultIfEmpty(0).Sum();
            }
        }

        public List<PurchaseOrderItem> PurchaseOrderItems { get; private set; }

        protected PurchaseOrder(string barcode, DateTimeOffset deliveryDate, Guid orderedById, Guid supplierId, Guid branchId)
        {
            
            Barcode = barcode;
            DeliveryDate = deliveryDate;
            OrderedById = orderedById;
            SupplierId = supplierId;
            BranchId = branchId;
            PurchaseOrderItems = new List<PurchaseOrderItem>();
        }


        public static DomainResult<PurchaseOrder> Create(string barcode, DateTimeOffset deliveryDate, Guid orderedById, Guid supplierId, Guid branchId, IDateTimeProvider dateTimeProvider)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                DomainResult<PurchaseOrder>.Error(PurchaseOrderErrorCodes.InvalidBarcode, $"'{nameof(barcode)}' cannot be null or empty.");
            }

            if (deliveryDate < dateTimeProvider.GetCurrentDateTimeOffset())
            {
                DomainResult<PurchaseOrder>.Error(PurchaseOrderErrorCodes.DeliveryDatePast, $"'{nameof(deliveryDate)}' is expired.");
            }

            var purchaseOrder = new PurchaseOrder(barcode, deliveryDate, orderedById, supplierId, branchId);
            return DomainResult<PurchaseOrder>.Success(purchaseOrder);
        }

    }
}
