using ONEReader.Build;
using ONEReader.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.DataHelper
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
        internal static string cleanNonBreakingSpace(this string strVar)
        {
            return strVar.Replace('\r', ' ').Replace('\t', ' ').Replace('\u00A0', ' ').Replace(Convert.ToChar(160), ' ').Replace(Convert.ToChar(194), ' ');
        }
        internal static string UnescapeValue(this string strVar)
        {
            return strVar.Replace('\r', ' ').Replace('\t', ' ').Replace('\u00A0', ' ').Replace(Convert.ToChar(160), ' ').Replace(Convert.ToChar(194), ' ').Replace('\n', ' ').Replace(" ", " ");
        }
        internal static string customRatesValue(this string strVar)
        {
            return strVar.Replace('\r', ' ').Replace('\t', ' ').Replace('\u00A0', ' ').Replace(Convert.ToChar(160), ' ').Replace(Convert.ToChar(194), ' ').Replace('\n', '@').Replace(" ", " ");
        }
        internal static string removeEnter(this string strVar)
        {
            if( strVar.Contains(Convert.ToChar(226)) && strVar.Contains(Convert.ToChar(134)) && strVar.Contains(Convert.ToChar(181)))
            {
                return strVar.Replace(Convert.ToChar(226), ' ').Replace(Convert.ToChar(134), ' ').Replace(Convert.ToChar(181), '@');
            }
            return strVar;
        }
        //internal static string customRatesValue2(this string strVar)
        //{
        //    return strVar.Replace('\t', ' ').Replace('\u00A0', ' ').Replace(Convert.ToChar(160), ' ').Replace(Convert.ToChar(194), ' ').Replace('\n', '@').Replace(" ", " ");
        //}
        internal static string removeQuotes(this string value)
        {
            return value.Replace('\'', ' ').Replace('\"', ' ').Trim();
        }
        internal static string removeSpaces(this string value)
        {
            return value.Replace(" ", "");
        }
        internal static string getCommodityValue(this string header)
        {
            return getLastString(header.ToLowerInvariant(), "commodity").ToUpper().Replace(":", "").Replace("  ", "").Trim();
        }
        internal static string getOrigin(this string header)
        {
            header = header.ToUpper().Replace("ORIGIN VIA", "VIA");
            if (header.ToLower().IndexOf("origin") != -1 && header.ToLower().IndexOf("via") != -1)
            {
                return getBetween(header.ToLower(), "origin", "via").ToUpper().Replace(":", "").Replace("  ", "").Trim();
            }
            else if (header.ToLower().IndexOf("origin") != -1)
            {
                return getLastString(header.ToLower(), "origin").ToUpper().Replace(":", "").Replace("  ", "").Trim();
            }
            else
                return "";
        }
        internal static string getApplicableOverValue(this string header)
        {
            return getLastString(header.ToLowerInvariant(), "over").ToUpper().Replace(":", "").Replace("  ", "").Trim();
        }
        internal static string getScopeValue(this string header)
        {
            if (header.ToLower().IndexOf("[") != -1)
                return getBetween(header.ToLower(), "[", "]").ToUpper().Replace(":", "").Replace("  ", "").Trim();
            else
                return "";
        }
        internal static string getBulletValue(this string header)
        {
            string bulletValueTemp = getFirstString(header.ToLowerInvariant(), "commodity");
            if( bulletValueTemp.ToLower().IndexOf("[") != -1)
            {
                //if scope found
                return getLastString(bulletValueTemp, "]").ToUpperInvariant().Replace(")", "");
            }
            else
            {
                //scope not found
                return bulletValueTemp.ToUpperInvariant().Replace(")", "");
            }
        }
        internal static string getService(this string value)
        {
            if (value.ToLowerInvariant().Replace(" ", "").Contains("(cy)")) return "CY";
            else if (value.ToLowerInvariant().Replace(" ", "").Contains("(rail)")) return "R";
            else if (value.ToLowerInvariant().Replace(" ", "").Contains("(door)")) return "D";
            else if (value.ToLowerInvariant().Replace(" ", "").Contains("(truck)")) return "D";
            else return "CY(DEFAULT)";
        }
        internal static string getService2(this string value)
        {
            if (value.ToLowerInvariant().Replace(" ", "").Contains("cy")) return "CY";
            else if (value.ToLowerInvariant().Replace(" ", "").Contains("door")) return "D";
            else if (value.ToLowerInvariant().Replace(" ", "").Contains("truck")) return "D";
            else return "CY(DEFAULT)";
        }
        private static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);

                if (End == -1)
                {
                    End = strSource.Length;
                }
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
       
        private static string getFirstString(string strSource, string strEnd)
        {
            int end;
            if (strSource.Contains(strEnd))
            {
                end = strSource.IndexOf(strEnd, 0);
                return strSource.Substring(0, end);
            }
            else
            {
                return "";
            }
        }
        private static string getLastString(string strSource, string strStart)
        {
            int start;
            if (strSource.Contains(strStart))
            {
                start = strSource.IndexOf(strStart, 0) + strStart.Length;
                return strSource.Substring(start);
            }
            else
            {
                return "";
            }

        }
        internal static string getOriginVia(this string header)
        {
            if (header.ToLower().IndexOf("via") != -1)
                return getLastString(header.ToLower(), "via").ToUpper().Replace(":", "").Replace("  ", "").Trim();
            else
                return "";
        }
        internal static void getNotes(this string footer, IPatternXML pattern, out string generalNotes, out string notesStr, out bool genExist)
        {
            footer = footer.cleanNonBreakingSpace();
            if (pattern.isKeyWordNotesExist(footer))
            {
                string match = "";
                foreach (string note in pattern.KEYWORDS_RATE_GENERALNOTE)
                {
                    if (footer.ToLower().Contains(note))
                    {
                        match = note;
                        break;
                    }
                }
                genExist = true;
                generalNotes = getLastString(footer.ToLower(), match).ToUpper().Replace(">", "").Replace("<", "").Replace("--", "").Trim();
                notesStr = getFirstString(footer.ToLower(), match).ToUpper().Replace(">", "").Replace("<", "").Trim();
            }
            else
            {
                genExist = false;
                generalNotes = "";
                notesStr = footer;
            }
        }
        internal static string getContainer(this string value, string br_cont_type)
        {
            string br_type = "DC";
            if (value.ToUpper().IndexOf("R2") > -1)
            {
                if (br_cont_type != null)
                {
                    if (br_cont_type == "DRY")
                    {
                        br_type = "NOR";
                    }
                    else
                    {
                        br_type = "RE";
                    }
                }
            }
            else if (value.ToUpper().IndexOf("R4") > -1)
            {
                if (br_cont_type != null)
                {
                    if (br_cont_type == "DRY")
                    {
                        br_type = "NOR";
                    }
                    else
                    {
                        br_type = "RE";
                    }
                }
            }
            else if (value.ToUpper().IndexOf("R5") > -1)
            {
                
                if (br_cont_type != null)
                {
                    if (br_cont_type == "DRY")
                    {
                        br_type = "NOR";
                    }
                    else
                    {
                        br_type = "RE";
                    }
                }
            }
            else if (value.ToUpper().IndexOf("F2") > -1)
            {
                br_type = "FR";
            }
            else if (value.ToUpper().IndexOf("F4") > -1)
            {
                br_type = "FR";
            }
            else if (value.ToUpper().IndexOf("O2") > -1)
            {
                br_type = "OT";
            }
            else if (value.ToUpper().IndexOf("O4") > -1)
            {
                br_type = "OT";
            }
            else if (value.ToUpper().IndexOf("T2") > -1)
            {
                br_type = "TK";
            }
            else if (value.ToUpper().IndexOf("T4") > -1)
            {
                br_type = "TK";
            }

            return br_type;
        }

        internal static bool IsCommodity(this string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("commodity:");
        }

        internal static bool IsOrigin(this string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("origin:");
        }

        internal static bool IsOriginVia(this string headderValue)
        {
            return headderValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("originvia:");
        }
        internal static bool IsScope(this string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("[") && headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("]");
        }
        internal  static bool IsApplicableOver(this string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("applicableover:");
        }
    }
}
