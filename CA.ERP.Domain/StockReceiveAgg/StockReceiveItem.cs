using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Types;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveItem : IEntity
    {
        public Guid Id { get; set; }


        public Guid MasterProductId { get; private set; }

        public Guid StockReceiveId { get; private set; }

        public Guid? PurchaseOrderItemId { get; private set; }

        public Guid? StockTransferItemId { get; private set; }

        public Guid? StockId { get; set; }

        public Guid BranchId { get; private set; }

        public string StockNumber { get; private set; }

        public StockReceiveItemStatus Status { get; private set; }

        public string SerialNumber { get; private set; }

        public decimal CostPrice { get; private set; }

        public string BrandName { get; private set; }

        public string Model { get; private set; }

        public StockReceiveItem()
        {

        }

        protected StockReceiveItem(Guid masterProductId, Guid stockReceiveId, Guid branchId, string stockNumber, string serialNumber, decimal costPrice, string brandName, string model, Guid? purchaseOrderItemId = null, Guid? stockTransferItemId = null)
        {
            Id = Guid.NewGuid();
            Status = StockReceiveItemStatus.Unknown;
            MasterProductId = masterProductId;
            StockReceiveId = stockReceiveId;
            PurchaseOrderItemId = purchaseOrderItemId;
            BranchId = branchId;
            StockNumber = stockNumber;
            SerialNumber = serialNumber;
            CostPrice = costPrice;
            BrandName = brandName;
            Model = model;
            StockTransferItemId = stockTransferItemId;
        }

        public DomainResult Commit(StockReceiveItemStatus status, string serialNumber = null)
        {
            if (status == StockReceiveItemStatus.Unknown)
            {
                return DomainResult.Error(StockReceiveErrorCodes.UnknownStockSource, "Unknown stock source is not allowed.");
            }

            if (serialNumber == string.Empty)
            {
                serialNumber = null;
            }

            Status = status;

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                SerialNumber = serialNumber;
            }
            
            return DomainResult.Success();
        }

        public static DomainResult<StockReceiveItem> CreateForPurchaseOrder(Guid masterProductId, Guid stockReceiveId, Guid purchaseOrderItemId, Guid branchId, decimal costPrice, string stockNumber, string brandName, string model)
        {
            DomainResult<StockReceiveItem> domainResult = validate(masterProductId, stockReceiveId, branchId, costPrice, stockNumber, purchaseOrderItemId: purchaseOrderItemId);

            if (domainResult != null)
            {
                return domainResult;
            }

            var item = new StockReceiveItem(masterProductId, stockReceiveId, branchId, stockNumber, null, costPrice, brandName, model, purchaseOrderItemId: purchaseOrderItemId);
            return DomainResult<StockReceiveItem>.Success(item);
        }

        public static DomainResult<StockReceiveItem> CreateForStockTransfer(Guid masterProductId, Guid stockReceiveId, Guid stockTransferItemId, Guid branchId, string stockNumber, string brandName, string model)
        {
            DomainResult<StockReceiveItem> domainResult = validate(masterProductId, stockReceiveId, branchId, 0, stockNumber, stockTransferItemId: stockTransferItemId);

            if (domainResult != null)
            {
                return domainResult;
            }

            var item = new StockReceiveItem(masterProductId, stockReceiveId, branchId, stockNumber, null, 0, brandName, model, stockTransferItemId: stockTransferItemId);
            return DomainResult<StockReceiveItem>.Success(item);
        }

        private static DomainResult<StockReceiveItem> validate(Guid masterProductId, Guid stockReceiveId, Guid branchId, decimal costPrice, string stockNumber, Guid? purchaseOrderItemId = null, Guid? stockTransferItemId = null)
        {
            DomainResult<StockReceiveItem> domainResult = null;
            if (purchaseOrderItemId != null && purchaseOrderItemId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvalidPurchaseOrderItemId, "Stock Receive Item Invalid Purchase Order Item Id");
            }

            if (stockTransferItemId != null && stockTransferItemId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvalidPurchaseOrderItemId, "Stock Receive Item Invalid Stock Transfer Item Id");
            }

            if (masterProductId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvaliMasterProductId, "Stock Receive Item Invalid Master Product Id");
            }
            if (stockReceiveId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.UnknownStockSource, "Stock Receive Item Unknow Source");
            }

            if (branchId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvaliBranchId, "Stock Receive Item Invalid Branch Id");
            }
            if (string.IsNullOrEmpty(stockNumber))
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.EmptyStockNumber, "Stock Receive Item stock number is empty");
            }
            if (costPrice < 0)
            {
                domainResult = DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.EmptyStockNumber, "Stock Receive Item cost price should be greater that zero(0).");
            }

            return domainResult;
        }
    }
}
