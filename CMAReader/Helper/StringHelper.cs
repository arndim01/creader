using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Helper
{
    public static class StringHelper
    {
        public static string RemoveUnnecessaryString(this string strVar) => strVar.Replace('\r', ' ').Replace('\t', ' ').Replace('\u00A0', ' ').Replace(Convert.ToChar(160), ' ').Replace(Convert.ToChar(194), ' ').Replace('\n', ' ').Replace(" ", "");
    }
}
