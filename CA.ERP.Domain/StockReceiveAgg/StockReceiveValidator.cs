using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.SupplierAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveValidator: AbstractValidator<StockReceive>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ISupplierRepository _supplierRepository;

        public StockReceiveValidator(IPurchaseOrderRepository purchaseOrderRepository, IBranchRepository branchRepository, ISupplierRepository supplierRepository, IValidator<Stock> stockValidator)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _branchRepository = branchRepository;
            _supplierRepository = supplierRepository;
            RuleFor(s => s.StockSouce).NotEqual(StockSource.Unknown).WithMessage($"Stock source must not be Unknown");
            RuleFor(s => s.PurchaseOrderId).MustAsync(purchaseOrderExist).WithMessage("Must exist");
            RuleFor(s => s.SupplierId).MustAsync(supplierExist).WithMessage("Must exist");
            RuleFor(s => s.BranchId).MustAsync(branchExist).WithMessage("Must exist");

            RuleFor(s => s.Stocks).NotEmpty().Must(NoDuplicateSerialNumber).WithMessage("Duplicate serial number");
            RuleFor(s => s.Stocks).NotEmpty().Must(NoDuplicateStockNumber).WithMessage("Duplicate stock number");

            RuleForEach(s => s.Stocks)
                .SetValidator(stockValidator);
        }

        private async Task<bool> supplierExist(Guid supplierId, CancellationToken cancellationToken)
        {
            return await _supplierRepository.ExistAsync(supplierId);
        }

        private bool NoDuplicateStockNumber(List<Stock> stocks)
        {
            return stocks.GroupBy(s => s.StockNumber).All(s => s.Count() == 1);
        }

        private bool NoDuplicateSerialNumber(List<Stock> stocks)
        {
            return stocks.GroupBy(s => s.SerialNumber).All(s => s.Count() == 1);
        }

        private async Task<bool> branchExist(Guid branchId, CancellationToken cancellationToken)
        {
            return await _branchRepository.ExistAsync(branchId, cancellationToken:cancellationToken);
        }

        private async Task<bool> purchaseOrderExist(Guid? purchaseOrderId, CancellationToken cancellationToken)
        {
            bool ret = true;
            if (purchaseOrderId != null)
            {
                ret = await _purchaseOrderRepository.ExistAsync(purchaseOrderId.Value, cancellationToken: cancellationToken);
            }
            return ret;
        }
    }
}
