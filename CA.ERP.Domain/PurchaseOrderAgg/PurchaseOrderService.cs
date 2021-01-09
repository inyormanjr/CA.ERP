using CA.ERP.Domain.Base;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderService : ServiceBase<PurchaseOrder>
    {
        private readonly IPurchaseOrderTotalCostPriceCalculator _purchaseOrderTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalCostPriceCalculator _purchaseOrderItemTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalQuantityCalculator _purchaseOrderItemTotalQuantityCalculator;
        private readonly IUserHelper _userHelper;
        private readonly IPurchaseOrderFactory _purchaseOrderFactory;
        private readonly IValidator<PurchaseOrder> _purchaseOrderValidator;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderService(
            IUnitOfWork unitOfWork,
            IPurchaseOrderTotalCostPriceCalculator purchaseOrderTotalCostPriceCalculator,
            IPurchaseOrderItemTotalCostPriceCalculator purchaseOrderItemTotalCostPriceCalculator,
            IPurchaseOrderItemTotalQuantityCalculator purchaseOrderItemTotalQuantityCalculator,
            IUserHelper userHelper,
            IPurchaseOrderFactory purchaseOrderFactory,
            IValidator<PurchaseOrder> purchaseOrderValidator,
            IPurchaseOrderRepository purchaseOrderRepository)
            :base(unitOfWork, purchaseOrderRepository, purchaseOrderValidator, userHelper)
        {
            _purchaseOrderTotalCostPriceCalculator = purchaseOrderTotalCostPriceCalculator;
            _purchaseOrderItemTotalCostPriceCalculator = purchaseOrderItemTotalCostPriceCalculator;
            _purchaseOrderItemTotalQuantityCalculator = purchaseOrderItemTotalQuantityCalculator;
            _userHelper = userHelper;
            _purchaseOrderFactory = purchaseOrderFactory;
            _purchaseOrderValidator = purchaseOrderValidator;
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<OneOf<Guid, List<ValidationFailure>>> CreatePurchaseOrder(DateTime deliveryDate, Guid supplierId, Guid branchId, List<PurchaseOrderItem> purchaseOrderItems, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            var purchaseOrder = _purchaseOrderFactory.Create(deliveryDate, supplierId, branchId, purchaseOrderItems);

            var validationResult = _purchaseOrderValidator.Validate(purchaseOrder);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                purchaseOrder.CreatedBy = _userHelper.GetCurrentUserId();
                purchaseOrder.UpdatedBy = _userHelper.GetCurrentUserId();
                ret = await _purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken: cancellationToken);
                await _unitOfWork.CommitAsync();
            }
            return ret;
        }

        public async override Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateAsync(Guid id, PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            //fill in for validation
            purchaseOrder.Barcode = "NotEmtpy";
            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                purchaseOrderItem.TotalCostPrice = _purchaseOrderItemTotalCostPriceCalculator.Calculate(purchaseOrderItem);
                purchaseOrderItem.TotalQuantity = _purchaseOrderItemTotalQuantityCalculator.Calculate(purchaseOrderItem);
            }

            purchaseOrder.TotalCostPrice = _purchaseOrderTotalCostPriceCalculator.Calculate(purchaseOrder, purchaseOrder.PurchaseOrderItems);

            var ret = await base.UpdateAsync(id, purchaseOrder, cancellationToken);
            await _unitOfWork.CommitAsync();
            return ret;
        }

        public virtual async Task<PaginationBase<PurchaseOrder>> GetManyAsync(DateTime startDate, DateTime endDate, int itemPerPage = 10, int page = 1, CancellationToken cancellationToken = default)
        {
            int skip = (page - 1) * itemPerPage;
            int take = itemPerPage;
            int count = await _purchaseOrderRepository.CountAsync(startDate, endDate, cancellationToken);
            IEnumerable<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.GetManyAsync(startDate, endDate, skip, take, cancellationToken);
            double totalPages = (double)count / (double)itemPerPage;
            return new PaginatedPurchaseOrders()
            {
                Data = purchaseOrders.ToList(),
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling(totalPages)
            };
        }

    }

}
