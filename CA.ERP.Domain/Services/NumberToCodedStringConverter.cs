using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface INumberToCodedStringConverter
    {
        string Covert(long number);
    }

    public class NumberToCodedStringConverter : INumberToCodedStringConverter
    {
        public string AvailableStrings => "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";

        public string Covert(long number)
        {
            StringBuilder code = new StringBuilder();

            do
            {
                int reminder = (int)getReminder(number);
                code.Append(AvailableStrings[reminder]);
                number = getQuotient(number);
            } while (number > 0);

            return code.ToString();
        }

        private long getReminder(long number)
        {
            return number % AvailableStrings.Length;
        }

        private long getQuotient(long number)
        {
            return number / AvailableStrings.Length;
        }
    }
}
