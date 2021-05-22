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
    public class Stock : IEntity
    {


        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        public Guid SupplierId { get; private set; }

        public Guid MasterProductId { get; private set; }

        public Guid BranchId { get; private set; }

        public string StockNumber { get; private set; }

        public string SerialNumber { get; private set; }

        public StockStatus StockStatus { get; private set; }

        public decimal CostPrice { get; private set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string SupplierName { get; private set; }

        public string BranchName { get; private set; }

        public string BrandName { get; private set; }

        public string Model { get; private set; }

        public Stock()
        {

        }

        protected Stock(Guid masterProductId, Guid supplierId, Guid branchId, string stockNumber, string serialNumber, StockStatus stockStatus, decimal costPrice, string supplierName, string brandName, string model)
        {
            Id = Guid.NewGuid();
            Status = Status.Active;
            MasterProductId = masterProductId;
            SupplierId = supplierId;
            StockNumber = stockNumber;
            SerialNumber = serialNumber;
            StockStatus = stockStatus;
            CostPrice = costPrice;
            SupplierName = supplierName;
            BrandName = brandName;
            Model = model;
            BranchId = branchId;
        }

        public static DomainResult<Stock> Create(Guid masterProductId, Guid supplierId, Guid branchId, string stockNumber, string serialNumber, decimal costPrice, string supplierName, string brandName, string model)
        {
            if (masterProductId == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockErrorCodes.InvaliMasterProductId, "Stock Invalid Master Product Id");
            }
            if (supplierId == Guid.Empty)
            {
                return DomainResult<Stock>.Error(StockErrorCodes.InvalidSuplier, "Invalid supplier");
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

            return new Stock(masterProductId, supplierId, branchId, stockNumber, serialNumber, StockStatus.Available, costPrice, supplierName, brandName, model);
            
        }
    }
}
