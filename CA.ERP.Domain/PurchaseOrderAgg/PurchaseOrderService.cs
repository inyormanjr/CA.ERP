using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.SupplierAgg;
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
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPurchaseOrderTotalCostPriceCalculator _purchaseOrderTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalCostPriceCalculator _purchaseOrderItemTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalQuantityCalculator _purchaseOrderItemTotalQuantityCalculator;
        private readonly IUserHelper _userHelper;
        private readonly IPurchaseOrderFactory _purchaseOrderFactory;
        private readonly IValidator<PurchaseOrder> _purchaseOrderValidator;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IBranchPermissionValidator<PurchaseOrder> _branchPermissionValidator;

        public PurchaseOrderService(
            IUnitOfWork unitOfWork,
            ISupplierRepository supplierRepository,
            IPurchaseOrderTotalCostPriceCalculator purchaseOrderTotalCostPriceCalculator,
            IPurchaseOrderItemTotalCostPriceCalculator purchaseOrderItemTotalCostPriceCalculator,
            IPurchaseOrderItemTotalQuantityCalculator purchaseOrderItemTotalQuantityCalculator,
            IUserHelper userHelper,
            IPurchaseOrderFactory purchaseOrderFactory,
            IValidator<PurchaseOrder> purchaseOrderValidator,
            IPurchaseOrderRepository purchaseOrderRepository,
            IBranchPermissionValidator<PurchaseOrder> branchPermissionValidator)
            :base(unitOfWork, purchaseOrderRepository, purchaseOrderValidator, userHelper)
        {
            _supplierRepository = supplierRepository;
            _purchaseOrderTotalCostPriceCalculator = purchaseOrderTotalCostPriceCalculator;
            _purchaseOrderItemTotalCostPriceCalculator = purchaseOrderItemTotalCostPriceCalculator;
            _purchaseOrderItemTotalQuantityCalculator = purchaseOrderItemTotalQuantityCalculator;
            _userHelper = userHelper;
            _purchaseOrderFactory = purchaseOrderFactory;
            _purchaseOrderValidator = purchaseOrderValidator;
            _purchaseOrderRepository = purchaseOrderRepository;
            _branchPermissionValidator = branchPermissionValidator;
        }
        public async Task<OneOf<Guid, List<ValidationFailure>, Forbidden>> CreatePurchaseOrder(DateTime deliveryDate, Guid supplierId, Guid branchId, List<PurchaseOrderItem> purchaseOrderItems, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, Forbidden> ret;
            var purchaseOrder = _purchaseOrderFactory.Create(deliveryDate, supplierId, branchId, purchaseOrderItems);

            var validationResult = _purchaseOrderValidator.Validate(purchaseOrder);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else if (!await _branchPermissionValidator.HasPermissionAsync(purchaseOrder))
            {
                ret = default(Forbidden);
            }
            else
            {
                purchaseOrder.CreatedBy = _userHelper.GetCurrentUserId();
                purchaseOrder.UpdatedBy = _userHelper.GetCurrentUserId();
                foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
                {
                    //update supplier stock price
                    await _supplierRepository.AddOrUpdateSupplierMasterProductCostPriceAsync(purchaseOrder.SupplierId, purchaseOrderItem.MasterProductId, purchaseOrderItem.CostPrice, cancellationToken);
                }
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

        public virtual async Task<PaginationBase<PurchaseOrder>> GetManyAsync(string barcode, DateTime? startDate, DateTime? endDate, int pageSize = 10, int page = 1, CancellationToken cancellationToken = default)
        {
            int skip = (page - 1) * pageSize;
            int take = pageSize;
            int count = await _purchaseOrderRepository.CountAsync(barcode, startDate, endDate, cancellationToken);
            IEnumerable<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.GetManyAsync(barcode, startDate, endDate, skip, take, cancellationToken);
            double totalPages = (double)count / (double)pageSize;
            return new PaginatedPurchaseOrders()
            {
                Data = purchaseOrders.ToList(),
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling(totalPages),
                PageSize = pageSize,
                TotalCount = count,
            };
        }

    }

}
