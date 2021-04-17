using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockAgg
{
    public class Stock: IEntity
    {
        

        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        public Guid MasterProductId { get; private set; }
        public Guid StockReceiveId { get; private set; }
        public Guid? PurchaseOrderItemId { get; private set; }
        public Guid BranchId { get; private set; }
        public string StockNumber { get; private set; }
        public string SerialNumber { get; private set; }
        public StockStatus StockStatus { get; private set; }
        public decimal CostPrice { get; private set; }

        public string BrandName { get; private set; }
        public string Model { get; private set; }
        

        protected Stock( Guid masterProductId, Guid stockReceiveId, Guid? purchaseOrderItemId, Guid branchId, string stockNumber, string serialNumber, StockStatus stockStatus, decimal costPrice, string brandName, string model)
        {
            Id = Guid.NewGuid();
            Status = Status.Active;
            MasterProductId = masterProductId;
            StockReceiveId = stockReceiveId;
            PurchaseOrderItemId = purchaseOrderItemId;
            StockNumber = stockNumber;
            SerialNumber = serialNumber;
            StockStatus = stockStatus;
            CostPrice = costPrice;
            BrandName = brandName;
            Model = model;
            BranchId = branchId;
        }

        public static DomainResult<Stock> Create(Guid masterProductId, Guid stockReceiveId, Guid? purchaseOrderItemId, Guid branchId, string stockNumber, string serialNumber, StockStatus stockStatus, decimal costPrice, string brandName, string model)
        {
            if (masterProductId == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockReceiveErrorCodes.InvaliMasterProductId, "Stock Invalid Master Product Id");
            }
            if (stockReceiveId == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockReceiveErrorCodes.UnknownStockSource, "Stock Receive Unknow Source");
            }
            if (purchaseOrderItemId != null && purchaseOrderItemId.Value == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockErrorCodes.InvalidPurchaseOrderItemId, "Stock Invalid Purchase Order Item Id");
            }
            if (branchId == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockErrorCodes.InvaliBranchId, "Stock Invalid Branch Id");
            }
            if (string.IsNullOrEmpty(stockNumber))
            {
                return DomainResult<Stock>.Error(StockErrorCodes.EmptyStockNumber, "Stock empty stock number");
            }
            if (serialNumber == string.Empty)
            {
                return DomainResult<Stock>.Error(StockErrorCodes.EmptySerialNumber, "Stock empty serial number");
            }

            var ret = new Stock(masterProductId, stockReceiveId, purchaseOrderItemId, branchId, stockNumber, serialNumber, stockStatus, costPrice, brandName, model);
            return DomainResult<Stock>.Success(ret);
        }
    }
}
