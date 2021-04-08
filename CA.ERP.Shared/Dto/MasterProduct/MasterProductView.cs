using CA.ERP.Common.Types;
using System;

namespace CA.ERP.WebApp.Dto.MasterProduct
{
    /// <summary>
    /// The main reference for the product
    /// </summary>
    public class MasterProductView : DtoViewBase
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Guid BrandId { get; set; }
    }
}
