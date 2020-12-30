using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class CreateBaseRequest<T> where T : class
    {
        /// <summary>
        /// The data to create
        /// </summary>
        /// 
        [Required]
        public T Data { get; set; }
    }
}
