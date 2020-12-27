using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Base class for update request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateBaseRequest<T> where T:DtoBase
    {
        /// <summary>
        /// The data to update
        /// </summary>
        /// 
        [Required]
        public T Data { get; set; }
    }
}
