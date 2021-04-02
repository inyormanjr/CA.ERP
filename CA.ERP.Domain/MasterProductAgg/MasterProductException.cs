using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProductException : AbstractDomainException
    {
        public MasterProductException(string code, string message) : base(code, message)
        {
        }

        public const string InvalidModel = "invalid-model";
        public const string EmptyBrandId = "empty-brand-id";
        public const string MasterProductNotFound = "master-product-not-found";

    }
}
