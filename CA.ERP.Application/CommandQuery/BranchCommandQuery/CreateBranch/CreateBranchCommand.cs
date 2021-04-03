using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.CreateBranch
{
    public class CreateBranchCommand : IRequest<DomainResult<Guid>>
    {
        public string Name { get;  set; }
        public int BranchNo { get;  set; }
        public string Code { get;  set; }
        public string Address { get;  set; }
        public string Contact { get;  set; }

        public CreateBranchCommand(string name, int branchNo, string code, string address, string contact)
        {
            Name = name;
            BranchNo = branchNo;
            Code = code;
            Address = address;
            Contact = contact;
        }
    }
}
