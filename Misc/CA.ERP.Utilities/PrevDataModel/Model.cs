using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class Model
    {
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public string BrandId { get; set; }
    }
}
