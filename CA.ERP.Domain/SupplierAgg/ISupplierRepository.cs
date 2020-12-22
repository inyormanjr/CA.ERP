using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.SupplierAgg
{
    public interface ISupplierRepository: IRepository<Supplier>
    {
    }
}