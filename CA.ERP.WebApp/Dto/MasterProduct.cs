using System;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// The main reference for the product
    /// </summary>
    public class MasterProduct : DtoBase
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Guid BrandId { get; set; }
    }
}