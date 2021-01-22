using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;

namespace CA.ERP.Domain.CustomerAgg
{
  public class CustomerService : ServiceBase<Customer>
  {
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IValidator<Customer> validator, IUserHelper userHelper) : base(unitOfWork, customerRepository, validator, userHelper)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OneOf<Guid, List<ValidationFailure>>> CreateCustomer(string firstName, string middleName, string lastName, string address, string employer, string employerAddress, string coMaker, string coMakerAddress, CancellationToken cancellationToken = default)
    {
        Customer customer = new Customer() {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            Address = address,
            Employer = employer,
            EmployerAddress = employerAddress,
            CoMaker = coMaker,
            CoMakerAddress = coMakerAddress,
        };

        OneOf<Guid, List<ValidationFailure>> ret;

        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid)
        {
            ret = validationResult.Errors.ToList();
        }else
        {
            ret = await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.CommitAsync();
        }

        return ret;
    }

    public async Task<PaginationBase<Customer>> GetManyAsync(string firstName, string lastname, int pageSize, int currentPage, CancellationToken cancellationToken)
    {
        int skip = (currentPage - 1) * pageSize;
        int take = pageSize;
        

        var count = await _customerRepository.CountAsync(firstName, lastname, cancellationToken);

        int totalPages = (int)Math.Ceiling((double)count / (double)pageSize);
    
        List<Customer> customers = await _customerRepository.GetManyAsync(firstName, lastname, skip, take, cancellationToken);

        return new PaginatedCustomer() {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = count,
            TotalPage = totalPages,
            Data = customers
        };
    }
  }
}