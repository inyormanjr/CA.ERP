using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto
{
    public class ValidationError
    {
        //
        // Summary:
        //     The name of the property.
        public string PropertyName { get; set; }
        //
        // Summary:
        //     The error message
        public string ErrorMessage { get; set; }
        //
        // Summary:
        //     The property value that caused the failure.
        public object AttemptedValue { get; set; }
    }
}
