using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Default response for getting list of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetManyResponse<T> where T:class
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        /// <summary>
        /// A list of data
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
