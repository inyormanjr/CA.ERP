using System.Collections.Generic;
using FluentValidation.Results;

namespace CA.ERP.Domain.Common.ResultTypes
{
    public struct ValidationError
    {
        public ValidationError(List<ValidationFailure> validationFailure)
        {
            ValidationFailure = validationFailure;
        }
        List<ValidationFailure> ValidationFailure;
    }
}