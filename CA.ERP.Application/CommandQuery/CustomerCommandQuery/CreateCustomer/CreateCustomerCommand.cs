using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.CustomerCommandQuery.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<DomainResult<Guid>>
    {
        public CustomerCreate Customer { get; set; }

        public CreateCustomerCommand(CustomerCreate customer)
        {
            Customer = customer;
        }
    }
}
