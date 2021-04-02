using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Core.DomainExceptions
{
    public abstract class AbstractDomainException : Exception
    {
        public string Code { get; set; }
        public AbstractDomainException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
