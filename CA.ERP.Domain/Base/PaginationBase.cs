using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Base
{
    /// <summary>
    /// Base for pagination on get all and searches
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class PaginationBase<TData> where TData: class
    {
        public ICollection<TData> Data;
    }
}
