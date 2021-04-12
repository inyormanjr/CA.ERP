using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Models
{
    public class Error
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public Uri Type { get; set; }

        public string Title { get; set; }

        public long Status { get; set; }

        public string TraceId { get; set; }
    }

}
