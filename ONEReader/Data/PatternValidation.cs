using ONEReader.Build;
using ONEReader.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public static class PatternValidation
    {
        internal static bool isMainSearchExist(this IPatternXML pattern, string sStore)
        {
            return pattern.MAINSEARCH.Any((sStore.Replace("\n", "").UnescapeValue().Replace(" ", "").ToLowerInvariant().ToString()).Contains);
        }
        internal static bool isKillSearchExist(this IPatternXML pattern, string sStore)
        {

            foreach (string keyword in pattern.KILL_SEARCH)
            {
                if (sStore.ToLowerInvariant().UnescapeValue().Replace(" ", "").Contains(keyword.ToLowerInvariant().UnescapeValue().Replace(" ", "")))
                    return true;
            }
            return false;
        }
        internal static bool isHeaderExist(this IPatternXML pattern, string sStore)
        {
            string[] keylist;
            foreach (string keyword in pattern.KEYWORDS)
            {
                keylist = keyword.ToLowerInvariant().Split(';');
                string cleanvalue = sStore.ToLowerInvariant().UnescapeValue().Replace(" ", "");
                bool checkvalid = keylist.All(cleanvalue.Contains);
                if (checkvalid) return true;
            }
            return false;
        }
        internal static bool isTableExist(this IPatternXML pattern, string sStore)
        {
            return sStore.UnescapeValue().ToLower().IndexOf(pattern.TABLESEARCH_RATES.ToLowerInvariant()) > -1 || sStore.UnescapeValue().ToLower().IndexOf(pattern.TABLESEARCH_ARBS.ToLowerInvariant()) > -1;
        }
        internal static bool isBlankValue(this string strStore)
        {
            return strStore.UnescapeValue().Replace(" ", "").ToUpperInvariant() == "BLANK" || strStore.UnescapeValue().Replace(" ", "") == "";
        }
        internal static bool isMainRatesHeader(this IPatternXML pattern, string sStore)
        {
            return pattern.MAINSEARCH_RATES.Any((sStore.UnescapeValue().Replace(" ", "").ToLowerInvariant()).Contains);
        }
        internal static bool isMainArbsHeader(this IPatternXML pattern, string sStore)
        {
            return pattern.MAINSEARCH_ARBS.Any((sStore.UnescapeValue().Replace(" ", "").ToLowerInvariant()).Contains);
        }
        internal static bool isArbsHeaderExist(this IPatternXML pattern, string sStore)
        {
            return sStore.ToLowerInvariant().UnescapeValue().Replace(" ", "").Contains("[") || sStore.ToLowerInvariant().UnescapeValue().Replace(" ","").Contains(pattern.TABLESEARCH_VIASEARCH) || pattern.isOriginArbsExist(sStore) || pattern.isDestinationArbsExist(sStore);
        }
        internal static bool isOriginArbsExist(this IPatternXML pattern, string sStore)
        {
            return sStore.ToLowerInvariant().UnescapeValue().Replace(" ", "").Contains(pattern.ORIGIN_ARBS.ToLowerInvariant());
        }
        internal static bool isDestinationArbsExist(this IPatternXML pattern, string sStore)
        {
            return sStore.ToLowerInvariant().UnescapeValue().Replace(" ", "").Contains(pattern.DESTINATION_ARBS.ToLowerInvariant());
        }
        internal static bool isKeyWordNotesExist(this IPatternXML pattern, string sStore)
        {
            return pattern.KEYWORDS_RATE_GENERALNOTE.Any(sStore.ToLowerInvariant().UnescapeValue().Contains);
        }
    }
}
