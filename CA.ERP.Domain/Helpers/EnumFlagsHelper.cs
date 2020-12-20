using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.Helpers
{
    public class EnumFlagsHelper : HelperBase
    {
        public List<String> ConvertToList(Enum @enum)
        {
            var ret = new List<string>();
            if (@enum.GetType().IsDefined(typeof(FlagsAttribute), true))
            {
                ret = @enum.ToString().Split(',').Select(flag => flag.Trim()).ToList();
            }
            else
            {
                ret.Add(@enum.ToString());
            }
            return ret;
        }
    }
}
