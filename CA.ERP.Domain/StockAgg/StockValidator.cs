using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public class StockValidator : AbstractValidator<Stock>
    {
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IPurchaseOrderItemRepository _purchaseOrderItemRepository;
        private readonly IStockRepository _stockRepository;

        public StockValidator(IMasterProductRepository masterProductRepository, IPurchaseOrderItemRepository purchaseOrderItemRepository, IStockRepository stockRepository)
        {
            _masterProductRepository = masterProductRepository;
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
            _stockRepository = stockRepository;

            RuleFor(s => s.StockNumber).NotEmpty().MustAsync(StockNumberNotExist).WithMessage("Duplicate stock number");
            RuleFor(s => s.SerialNumber).NotEmpty().MustAsync(SerialNumberNotExist).WithMessage("Duplicate serial number");
            RuleFor(s => s.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(s => s.MasterProductId).MustAsync(MasterProductExist).WithMessage("MasterProduct must exist"); ;
            RuleFor(s => s.PurchaseOrderItemId).MustAsync(PurchaseOrderNotExist).WithMessage("Stock number must exist"); ;

        }

        private async Task<bool> StockNumberNotExist(string stockNumber,  CancellationToken cancellationToken)
        {
            return !(await _stockRepository.StockNumberExist(stockNumber));
        }

        private async Task<bool> SerialNumberNotExist(string serialNumber, CancellationToken cancellationToken)
        {
            return !(await _stockRepository.SerialNumberExist(serialNumber));
        }

        private async Task<bool> MasterProductExist(Guid masterProductId, CancellationToken cancellationToken)
        {
            return await _masterProductRepository.ExistAsync(masterProductId, cancellationToken:cancellationToken);
           
        }

        private async Task<bool> PurchaseOrderNotExist(Guid? purchaseOrderId, CancellationToken cancellationToken)
        {
            bool ret = true;
            if (purchaseOrderId != null)
            {
                ret = await _purchaseOrderItemRepository.ExistAsync(purchaseOrderId.Value, cancellationToken: cancellationToken);
               
            }
            return ret;
        }
    }
}
