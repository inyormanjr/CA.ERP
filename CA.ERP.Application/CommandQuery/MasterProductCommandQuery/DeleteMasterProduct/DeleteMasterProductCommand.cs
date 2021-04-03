using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.DeleteMasterProduct
{
    public class DeleteMasterProductCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }

        public DeleteMasterProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
