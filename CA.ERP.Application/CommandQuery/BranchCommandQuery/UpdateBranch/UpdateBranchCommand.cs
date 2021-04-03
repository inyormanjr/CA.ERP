using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.UpdateBranch
{
    public class UpdateBranchCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public UpdateBranchCommand(Guid id, string name, int branchNo, string code, string address, string contact)
        {
            Id = id;
            Name = name;
            BranchNo = branchNo;
            Code = code;
            Address = address;
            Contact = contact;
        }
    }
}
