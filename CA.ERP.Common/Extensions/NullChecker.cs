using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Extensions
{
    public static class NullChecker
    {
        public static bool IsNull(this object obj)
        {
            return obj == null ? true : false;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static void ThrowIfNullArgument(this object obj, string parameterName)
        {
            if (obj.IsNull())
            {
                throw new ArgumentNullException($"Argument {parameterName} is null.");
            }
        }
    }
}
