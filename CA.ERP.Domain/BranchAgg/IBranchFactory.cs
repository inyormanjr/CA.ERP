using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.BranchAgg
{
    public interface IBranchFactory : IFactory<Branch>
    {
        Branch Create(string name, int branchNo, string code, string address, string contact);
    }
}