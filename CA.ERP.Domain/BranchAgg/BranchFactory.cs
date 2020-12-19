namespace CA.ERP.Domain.BranchAgg
{
    public class BranchFactory : IBranchFactory
    {
        public Branch Create(string name, int branchNo, string code, string address, string contact)
        {
            return new Branch() { Name = name, BranchNo = branchNo, Code = code , Address = address, Contact = contact };
        }
    }
}