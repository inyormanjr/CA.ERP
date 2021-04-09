using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Models
{
    public class Pagination
    {
        public int Skip
        {
            get
            {
                return Page * Size;
            }
        }
        public int Take {
            get
            {
                return Size;
            }
        }
        public int Page { get; set; }
        public int Size { get; set; }
        public Pagination(int page, int size)
        {
            Page = page;
            Size = size;
        }

    }
}
