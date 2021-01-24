using System.Collections.Generic;
using FluentValidation.Results;

namespace CA.ERP.Domain.Common.ResultTypes
{
    public struct ValidationError
    {
        List<ValidationFailure> ValidationFailure;
    }
}