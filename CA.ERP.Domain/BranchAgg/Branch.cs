using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BranchAgg
{
    public class Branch :IEntity
    {
        public string Name { get; private set; }
        public int BranchNo { get; private set; }
        public string Code { get; private set; }
        public string Address  { get; private set; }
        public string Contact { get; private set; }

        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        public Branch()
        {

        }
        private Branch(string name, int branchNo, string code, string address, string contact)
        {
            Name = name;
            BranchNo = branchNo;
            Code = code;
            Address = address;
            Contact = contact;
        }

        public DomainResult Update(string name, int branchNo, string code, string address, string contact)
        {
            if (string.IsNullOrEmpty(name))
            {
                return DomainResult<Branch>.Error(ErrorType.Error, BranchErrorCodes.InvalidName, $"'{nameof(name)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(code))
            {
                return DomainResult<Branch>.Error(BranchErrorCodes.InvalidCode, $"'{nameof(code)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(address))
            {
                return DomainResult<Branch>.Error(BranchErrorCodes.InvalidAddress, $"'{nameof(address)}' cannot be null or empty.");
            }

            Name = name;
            BranchNo = branchNo;
            Code = code;
            Address = address;
            Contact = contact;


            return DomainResult.Success();
        }


        public static DomainResult<Branch> Create(string name, int branchNo, string code, string address, string contact)
        {
            if (string.IsNullOrEmpty(name))
            {
                return DomainResult<Branch>.Error(ErrorType.Error,BranchErrorCodes.InvalidName, $"'{nameof(name)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(code))
            {
                return DomainResult<Branch>.Error(BranchErrorCodes.InvalidCode, $"'{nameof(code)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(address))
            {
                return DomainResult<Branch>.Error(BranchErrorCodes.InvalidAddress, $"'{nameof(address)}' cannot be null or empty.");
            }


            var branch = new Branch(name, branchNo, code, address, contact);
            return DomainResult<Branch>.Success(branch);
        }
    }
}
