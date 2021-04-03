using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core
{
    // <summary>
    /// Base for pagination on get all and searches
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class PaginatedList<TData> where TData : class
    {
        public int TotalCount { get; set; }
        public ICollection<TData> Data;
    }

}
