using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto
{
    /// <summary>
    /// Default response for getting list of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResponse<T> where T:class
    {
        public int TotalCount { get; set; }
        /// <summary>
        /// A list of data
        /// </summary>
        public IEnumerable<T> Data { get; set; }


    }
}
