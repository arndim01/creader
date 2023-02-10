using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter.Data
{
    public static class StringHelper
    {
        internal static string convertDateFormat(this DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                return String.Format("{0:MM/dd/yyyy}", date);
            }
            else
            {
                return "";
            }
        }
    }
}
