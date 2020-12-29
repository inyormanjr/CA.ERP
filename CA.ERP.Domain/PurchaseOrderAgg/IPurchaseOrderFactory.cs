using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderFactory : IFactory<PurchaseOrder>
    {
        PurchaseOrder Create(string barcode, System.DateTime deliveryDate, System.Guid approvedById, System.Guid supplierId, System.Guid branchId);
    }
}