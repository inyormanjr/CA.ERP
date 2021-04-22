using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceive : IEntity
    {


        public Guid Id { get; private set; }
        public Status Status { get; private set; }
        public DateTimeOffset? DateReceived { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public Guid? PurchaseOrderId { get; private set; }
        public Guid BranchId { get; private set; }
        public StockSource StockSouce { get; private set; }
        public StockReceiveStage Stage { get; private set; }
        public string DeliveryReference { get; private set; }
        public Guid SupplierId { get; private set; }

        public string SupplierName { get; set; }
        public string BranchName { get; set; }

        public List<StockReceiveItem> Items { get; set; } = new List<StockReceiveItem>();
        public StockReceive()
        {

        }

        protected StockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, DateTimeOffset dateCreated, StockReceiveStage stage)
        {
            Id = Guid.NewGuid();
            Status = Status.Active;
            PurchaseOrderId = purchaseOrderId;
            BranchId = branchId;
            StockSouce = stockSource;
            SupplierId = supplierId;
            DateCreated = dateCreated;
            Stage = stage;
        }

        public void AddItem(StockReceiveItem item)
        {
            Items.Add(item);
        }

        public static DomainResult<StockReceive> Create(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, IDateTimeProvider dateTimeProvider)
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
            var ret = new StockReceive(purchaseOrderId, branchId, stockSource, supplierId, dateTimeProvider.GetCurrentDateTimeOffset(), StockReceiveStage.Intial);
            return DomainResult<StockReceive>.Success(ret);
        }

        public void Commit(IDateTimeProvider dateTimeProvider)
        {
            Stage = StockReceiveStage.Commited;
            DateReceived = dateTimeProvider.GetCurrentDateTimeOffset();
        }


    }
}
