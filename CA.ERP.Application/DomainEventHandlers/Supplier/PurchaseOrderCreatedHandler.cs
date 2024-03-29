using CA.ERP.Domain.Core;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.SupplierAgg;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Application.DomainEventHandlers.Supplier
{
    public class PurchaseOrderCreatedHandler : IConsumer<PurchaseOrderCreated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierRepository _supplierRepository;

        public PurchaseOrderCreatedHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _supplierRepository = supplierRepository;
        }
        public async Task Consume(ConsumeContext<PurchaseOrderCreated> context)
        {
            //update supplier prices

            context.TryGetPayload(out PurchaseOrder purchaseOrder);
            foreach (var item in purchaseOrder.PurchaseOrderItems)
            {
                var createSupplierMasterProduct = SupplierMasterProduct.Create(item.MasterProductId, purchaseOrder.SupplierId, item.CostPrice);
                if (createSupplierMasterProduct.IsSuccess)
                {
                    await _supplierRepository.AddOrUpdateAsync(createSupplierMasterProduct.Result, context.CancellationToken);
                }

            }
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
