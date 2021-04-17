using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.SupplierAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceive: IEntity
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSouce { get; set; }
        public string DeliveryReference { get; set; }
        public Guid SupplierId { get; set; }

        protected StockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, string deliveryReference)
        {

        }

        public static DomainResult<StockReceive> Create(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, string deliveryReference)
        {
            if (purchaseOrderId != null && purchaseOrderId.Value == Guid.Empty)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.InvalidPurchaseOrderId, "Stock Receive Invalid Purchase Order Id");
            }
            if (branchId == Guid.Empty)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.InvaliBranchId, "Stock Receive Invalid  Branch Id");
            }
            if (supplierId == Guid.Empty)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.InvaliSupplierId, "Stock Receive Invalid Supplier Id");
            }
            if (stockSource == StockSource.Unknown)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.UnknownStockSource, "Stock Receive Unknow Source");
            }
            var ret = new StockReceive(purchaseOrderId, branchId, stockSource, supplierId, deliveryReference);
            return DomainResult<StockReceive>.Success(ret);
        }
    }
}
