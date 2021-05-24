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

        public Guid? StockTransferId { get; private set; }

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

        protected StockReceive(Guid id, Guid branchId, StockSource stockSource, Guid supplierId, DateTimeOffset dateCreated, StockReceiveStage stage, Guid? purchaseOrderId)
        {
            Id = id;
            Status = Status.Active;
            BranchId = branchId;
            StockSouce = stockSource;
            SupplierId = supplierId;
            DateCreated = dateCreated;
            Stage = stage;
            PurchaseOrderId = purchaseOrderId;
        }

        public void AddItem(StockReceiveItem item)
        {
            Items.Add(item);
        }

        public static DomainResult<StockReceive> CreateForPurchaseOrder(Guid branchId, StockSource stockSource, Guid supplierId, IDateTimeProvider dateTimeProvider, Guid? purchaseOrderId)
        {
            DomainResult<StockReceive> domainResult = validate(branchId, stockSource, supplierId);

            if (domainResult != null)
            {
                return domainResult;
            }

            return new StockReceive(Guid.NewGuid(), branchId, stockSource, supplierId, dateTimeProvider.GetCurrentDateTimeOffset(), StockReceiveStage.Pending, purchaseOrderId);

        }


        private static DomainResult<StockReceive> validate(Guid branchId, StockSource stockSource, Guid supplierId)
        {
            DomainResult<StockReceive> domainResult = null;

            if (branchId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceive>.Error(StockReceiveErrorCodes.InvaliBranchId, "Stock Receive Invalid  Branch Id");
            }
            if (supplierId == Guid.Empty)
            {
                domainResult = DomainResult<StockReceive>.Error(StockReceiveErrorCodes.InvaliSupplierId, "Stock Receive Invalid Supplier Id");
            }
            if (stockSource == StockSource.Unknown)
            {
                domainResult = DomainResult<StockReceive>.Error(StockReceiveErrorCodes.UnknownStockSource, "Stock Receive Unknow Source");
            }

            return domainResult;
        }

        public bool IsCommitted()
        {
            return Stage == StockReceiveStage.Commited;
        }

        public void Commit(IDateTimeProvider dateTimeProvider)
        {
            Stage = StockReceiveStage.Commited;
            DateReceived = dateTimeProvider.GetCurrentDateTimeOffset();
        }


    }
}
