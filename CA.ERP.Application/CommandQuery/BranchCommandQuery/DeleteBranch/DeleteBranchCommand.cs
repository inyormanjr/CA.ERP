using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.DeleteBranch
{
    public class DeleteBranchCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }

        public DeleteBranchCommand(Guid id)
        {
            Id = id;
        }
    }
}
