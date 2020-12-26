using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Request body for creating master product
    /// </summary>
    public class CreateMasterProductRequest
    {
        public string Description { get; set; }
        public ProductStatus ProductStatus { get;  set; }
        public Guid BrandId { get;  set; }
        public string Model { get;  set; }
        
    }
}
