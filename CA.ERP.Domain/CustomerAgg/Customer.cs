using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using System;

namespace CA.ERP.Domain.CustomerAgg
{
    public class Customer : IEntity
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Employer { get; private set; }
        public string EmployerAddress { get; private set; }
        public string CoMaker { get; private set; }
        public string CoMakerAddress { get; private set; }

        private Customer(Guid id, string firstName, string middleName, string lastName, string address, string employer, string employerAddress, string coMaker, string coMakerAddress)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            Employer = employer;
            EmployerAddress = employerAddress;
            CoMaker = coMaker;
            CoMakerAddress = coMakerAddress;
        }

        public static DomainResult<Customer> Create(string firstName, string middleName, string lastName, string address, string employer, string employerAddress, string coMaker, string coMakerAddress)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.FirstNameRequired, "Customer first name is required");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.LastNameRequired, "Customer last name is required");
            }

            if (string.IsNullOrEmpty(address))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.AddressRequired, "Customer address is required");
            }

            if (string.IsNullOrEmpty(employer))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.EmployerRequired, "Customer employer is required");
            }

            if (string.IsNullOrEmpty(employerAddress))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.EmployerAddressRequired, "Customer employer address is required");
            }

            if (string.IsNullOrEmpty(coMaker))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.CoMakerRequired, "Customer co-maker is required");
            }

            if (string.IsNullOrEmpty(coMakerAddress))
            {
                return DomainResult<Customer>.Error(CustomerErrorCodes.CoMakerAddressRequired, "Customer co-maker address is required");
            }

            var customer = new Customer(Guid.NewGuid(), firstName, middleName, lastName, address, employer, employerAddress, coMaker, coMakerAddress);

            return DomainResult<Customer>.Success(customer);
        }
    }
}
