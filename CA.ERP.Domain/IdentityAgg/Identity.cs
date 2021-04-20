using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core.DomainResullts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.IdentityAgg
{
    public class Identity
    {
        public Guid Id { get; private set; }
        public IEnumerable<Guid> Branches { get; private set; }
        public IEnumerable<string> Roles { get; private set; }

        private Identity(Guid id, IEnumerable<Guid> branches, IEnumerable<string> roles)
        {
            Id = id;
            Branches = branches;
            Roles = roles;
        }



        public static DomainResult<Identity> Create(Guid id, IEnumerable<Guid> branches, IEnumerable<string> roles)
        {
            if (id == Guid.Empty)
            {
                return DomainResult<Identity>.Error(IdentityErrorCodes.EmptyId, "Identity id is empty");
            }
            var identity = new Identity(id, branches, roles);
            return DomainResult<Identity>.Success(identity);
        }

        public bool BelongsToBranch(Guid branchIdToCheck)
        {
            return Branches.Any(brandId => brandId == branchIdToCheck);
        }
    }
}
