using ClosedXML.Excel;
using ONEReader.Build;
using ONEReader.Data;
using ONEReader.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ONEReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtractTables(@"C:\Users\nel\Downloads\ONE RICN00004 Floating - Amd. 49 - Eff. Nov 20, 18.xlsx");
            //testCommoditySearch();
        }
        
        private static List<string> findCommodityWithSuggest(string value, string find, char key, string endfind, char endKey)
        {
            List<string> suggested = new List<string>();
            MatchCollection matchesFirst = Regex.Matches(value, find);
            MatchCollection matchesEnd = Regex.Matches(value, endfind);
            foreach (Match mf in matchesFirst)
            {
                string getSuggestedValue = "";
                if( matchesEnd.Count > 0)
                {
                    foreach (Match me in matchesEnd)
                    {
                        int mfIndex = findIndexLocation(mf.Index, value, find, key);
                        int meIndex = findIndexLocation(me.Index, value, endfind, endKey);
                        if (mfIndex != -1 && meIndex != -1)
                        {
                            int firstIndex = mfIndex - (mfIndex - mf.Index);
                            int secondIndex = meIndex - (meIndex - me.Index);
                            getSuggestedValue = value.Substring(firstIndex, secondIndex - firstIndex);
                            suggested.Add(getSuggestedValue);
                        }
                        //if (mfIndex != -1 && meIndex == -1)
                        //{
                        //    int firstIndex = mfIndex - (mfIndex - mf.Index);
                        //    getSuggestedValue = value.Substring(firstIndex, value.Length - firstIndex);
                        //    suggested.Add(getSuggestedValue);
                        //}
                    }
                }
                else
                {
                    int mfIndex = findIndexLocation(mf.Index, value, find, key);

                    if(  mfIndex != -1)
                    {
                        int firstIndex = mfIndex - (mfIndex - mf.Index);
                        getSuggestedValue = value.Substring(firstIndex, value.Length - firstIndex);
                        suggested.Add(getSuggestedValue);
                    }
                        
                } 
            }

            return suggested;
        }
        private static int findIndexLocation(int charPosition, string value, string find, char key)
        {

            int previousPosition = charPosition;

            for(; charPosition < value.Length; charPosition++)
            {
                charPosition += find.Length;
                for(; charPosition < value.Length; charPosition++)
                {
                    if (Char.IsWhiteSpace(value[charPosition])) continue;

                    if( value[charPosition] == key)
                    {
                        return charPosition ;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return -1;
        }

        public static void ExtractTables(string filePath)
        {
            
            ContractInfo info = new ContractInfo();
            info.carrier = "ONE";
            info.contractId = "TEST_ID";    
            info.amdId = "0";
            info.eff = Convert.ToDateTime("4/1/2018");
            info.exp = Convert.ToDateTime("4/30/2018");

            using (ContractProperties cProperties = new ContractProperties(info, filePath))
            {
                foreach (IXLWorksheet sheet in cProperties.workbook.Worksheets)
                {
                    cProperties.contractSheets.Add(sheet.Name);
                }
                foreach (IContractsheet contract in cProperties.contractSheets)
                {
                    using (Assemble assemble = new Assemble(cProperties.pattern, contract))
                    {
                        assemble.ReadData();
                        
                        
                        //assemble.writeData();
                    }
                }
            }
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
