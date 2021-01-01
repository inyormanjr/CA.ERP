using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.RdlcDesign.Models
{
    public class ModelBase
    {
        public ModelBase()
        {

            CreatedBy = Guid.Empty;
            UpdatedBy = Guid.Empty;
            Status = 1;
        }
        public Guid Id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
