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

        public Status Status { get; set; }

        public Guid MasterProductId { get; private set; }
        public Guid StockReceiveId { get; private set; }
        public Guid? PurchaseOrderItemId { get; private set; }
        public Guid BranchId { get; private set; }
        public string StockNumber { get; private set; }
        public StockStatus StockStatus { get; private set; }
        public string SerialNumber { get; private set; }
        public decimal CostPrice { get; private set; }

        public string BrandName { get; private set; }
        public string Model { get; private set; }
        public StockReceiveItem()
        {

        }
        protected StockReceiveItem(Guid masterProductId, Guid stockReceiveId, Guid? purchaseOrderItemId, Guid branchId, string stockNumber, string serialNumber, decimal costPrice, string brandName, string model, StockStatus stockStatus)
        {
            Id = Guid.NewGuid();
            Status = Status.Active;
            MasterProductId = masterProductId;
            StockReceiveId = stockReceiveId;
            PurchaseOrderItemId = purchaseOrderItemId;
            BranchId = branchId;
            StockNumber = stockNumber;
            SerialNumber = serialNumber;
            CostPrice = costPrice;
            BrandName = brandName;
            Model = model;
            StockStatus = stockStatus;
        }
        public static DomainResult<StockReceiveItem> Create(Guid masterProductId, Guid stockReceiveId, Guid? purchaseOrderItemId, Guid branchId, string stockNumber, string brandName, string model)
        {
            if (masterProductId == Guid.Empty)
            {
                return DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvaliMasterProductId, "Stock Receive Item Invalid Master Product Id");
            }
            if (stockReceiveId == Guid.Empty)
            {
                return DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.UnknownStockSource, "Stock Receive Item Unknow Source");
            }
            if (purchaseOrderItemId != null && purchaseOrderItemId.Value == Guid.Empty)
            {
                return DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvalidPurchaseOrderItemId, "Stock Receive Item Invalid Purchase Order Item Id");
            }
            if (branchId == Guid.Empty)
            {
                return DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.InvaliBranchId, "Stock Receive Item Invalid Branch Id");
            }
            if (string.IsNullOrEmpty(stockNumber))
            {
                return DomainResult<StockReceiveItem>.Error(StockReceiveErrorCodes.EmptyStockNumber, "Stock Receive Item stock number is empty");
            }


            var item = new StockReceiveItem(masterProductId, stockReceiveId, purchaseOrderItemId, branchId, stockNumber, string.Empty, 0, brandName, model, StockStatus.Unknown);
            return DomainResult<StockReceiveItem>.Success(item);
        }

    }
}
