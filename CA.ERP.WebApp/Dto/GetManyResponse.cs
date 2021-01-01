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
        /// <summary>
        /// A list of data
        /// </summary>
        public ICollection<T> Data { get; set; }
    }
}
