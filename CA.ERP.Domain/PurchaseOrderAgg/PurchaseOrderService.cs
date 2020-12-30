using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderService : ServiceBase
    {
        private readonly IPurchaseOrderBarcodeGenerator _purchaseOrderBarcodeGenerator;
        private readonly IPurchaseOrderTotalCostPriceCalculator _purchaseOrderTotalCostPriceCalculator;
        private readonly IPurchaseOrderItemTotalCostPriceCalculator _purchaseOrderItemTotalCalculator;
        private readonly IUserHelper _userHelper;
        private readonly IPurchaseOrderFactory _purchaseOrderFactory;
        private readonly IValidator<PurchaseOrder> _purchaseOrderValidator;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderService(
            IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator,
            IPurchaseOrderTotalCostPriceCalculator purchaseOrderTotalCostPriceCalculator,
            IPurchaseOrderItemTotalCostPriceCalculator purchaseOrderItemTotalCalculator,
            IUserHelper userHelper,
            IPurchaseOrderFactory purchaseOrderFactory,
            IValidator<PurchaseOrder> purchaseOrderValidator,
            IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderBarcodeGenerator = purchaseOrderBarcodeGenerator;
            _purchaseOrderTotalCostPriceCalculator = purchaseOrderTotalCostPriceCalculator;
            _purchaseOrderItemTotalCalculator = purchaseOrderItemTotalCalculator;
            _userHelper = userHelper;
            _purchaseOrderFactory = purchaseOrderFactory;
            _purchaseOrderValidator = purchaseOrderValidator;
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<OneOf<Guid, List<ValidationFailure>>> CreatePurchaseOrder(DateTime deliveryDate, Guid supplierId, Guid branchId, List<PurchaseOrderItem> purchaseOrderItems, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>> ret = Guid.Empty;
            var purchaseOrder = _purchaseOrderFactory.Create(deliveryDate, supplierId, branchId, purchaseOrderItems);
            


            var validationResult = _purchaseOrderValidator.Validate(purchaseOrder);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                ret = await _purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken: cancellationToken);
            }
            return ret;
        }
    }

}
