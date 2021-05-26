using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.StockTransferAgg
{
    public class StockTransfer : IEntity
    {
        public Guid Id { get; private set; }

        public Guid SourceBranchId { get; private set; }

        public string SourceBranchName { get; set; }

        public Guid DestinationBranchId { get; private set; }

        public string DestinationBranchName { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public Guid CreatedBy { get; private set; }

        public string CreatedByName { get; private set; }

        public ICollection<StockTransferItem> Items { get; set; } = new List<StockTransferItem>();

        public StockTransfer()
        {

        }

        private StockTransfer(Guid id, Guid sourceBranchId, Guid destinationBranchId, DateTimeOffset deliveryDate, Guid createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            SourceBranchId = sourceBranchId;
            DestinationBranchId = destinationBranchId;
            DeliveryDate = deliveryDate;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            
        }

        public DomainResult AddItem(StockTransferItem stockTransferItem)
        {
            stockTransferItem.ThrowIfNullArgument(nameof(stockTransferItem));

            if (!Items.Any(i => i.MasterProductId == stockTransferItem.MasterProductId))
            {
                Items.Add(stockTransferItem);
                return DomainResult.Success();
            }
            else
            {
                return DomainResult.Error(StockTransferErrorCodes.DuplicateItem, string.Format("There is a duplicate item with model: {0}", stockTransferItem.Model));
            }
        }

        public static DomainResult<StockTransfer> Create(Guid sourceBranchId, Guid destinationBranchId, DateTimeOffset deliveryDate, Guid createdBy, IDateTimeProvider datetimeProvider)
        {
            if (sourceBranchId == Guid.Empty)
            {
                return DomainResult<StockTransfer>.Error(StockTransferErrorCodes.InvalidSourceBranch, "Invalid source branch");
            }
            if (destinationBranchId == Guid.Empty)
            {
                return DomainResult<StockTransfer>.Error(StockTransferErrorCodes.InvalidDestinationBranch, "Invalid destination branch");
            }
            if (createdBy == Guid.Empty)
            {
                return DomainResult<StockTransfer>.Error(StockTransferErrorCodes.InvalidCreator, "Invalid creator");
            }
            if (deliveryDate.Date < datetimeProvider.GetCurrentDateTimeOffset().Date)
            {
                return DomainResult<StockTransfer>.Error(StockTransferErrorCodes.PastDeliveryDate, "Delivery date has past");
            }

            return new StockTransfer(Guid.NewGuid(), sourceBranchId, destinationBranchId, deliveryDate, createdBy, datetimeProvider.GetCurrentDateTimeOffset());
        }
    }

}
