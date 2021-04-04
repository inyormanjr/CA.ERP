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
        public Guid DestinationBranchId { get; private set; }

        public decimal GetTotalFreeQuantity
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
            DestinationBranchId = branchId;
            PurchaseOrderItems = new List<PurchaseOrderItem>();
        }

        public DomainResult Update(DateTimeOffset deliveryDate, Guid orderedById, Guid supplierId, Guid branchId, IDateTimeProvider dateTimeProvider)
        {
            //only the creator can edit 
            if (orderedById != OrderedById)
            {
                return DomainResult.Error(ErrorType.Forbidden, PurchaseOrderErrorCodes.DenyOtherUser, "Can't update other user's purchase order");
            }

            if (deliveryDate < dateTimeProvider.GetCurrentDateTimeOffset())
            {
                return DomainResult.Error(PurchaseOrderErrorCodes.DeliveryDatePast, $"'{nameof(deliveryDate)}' is expired.");
            }

            DeliveryDate = deliveryDate;
            SupplierId = supplierId;
            DestinationBranchId = branchId;

            return DomainResult.Success();
        }

        public void AddPurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            PurchaseOrderItems.Add(purchaseOrderItem);
        }


        public static DomainResult<PurchaseOrder> Create(DateTimeOffset deliveryDate, Guid orderedById, Guid supplierId, Guid branchId, IDateTimeProvider dateTimeProvider, IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator)
        {
            if (deliveryDate < dateTimeProvider.GetCurrentDateTimeOffset())
            {
               return  DomainResult<PurchaseOrder>.Error(PurchaseOrderErrorCodes.DeliveryDatePast, $"'{nameof(deliveryDate)}' is expired.");
            }


            var purchaseOrder = new PurchaseOrder(purchaseOrderBarcodeGenerator.GenerateBarcode(), deliveryDate, orderedById, supplierId, branchId);
            return DomainResult<PurchaseOrder>.Success(purchaseOrder);
        }

    }
}
