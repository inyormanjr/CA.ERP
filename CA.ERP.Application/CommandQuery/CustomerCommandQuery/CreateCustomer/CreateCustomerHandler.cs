using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.CustomerAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.CustomerCommandQuery.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<DomainResult<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var dtoCustomer = request.Customer;
            var createCustomerResult = Customer.Create(dtoCustomer.FirstName, dtoCustomer.MiddleName, dtoCustomer.LastName, dtoCustomer.Address, dtoCustomer.Employer, dtoCustomer.EmployerAddress, dtoCustomer.CoMaker, dtoCustomer.CoMakerAddress);

            if (!createCustomerResult.IsSuccess)
            {
                return createCustomerResult.ConvertTo<Guid>();
            }

            var id = await _customerRepository.AddAsync(createCustomerResult.Result, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return DomainResult<Guid>.Success(id);
        }
    }
}
