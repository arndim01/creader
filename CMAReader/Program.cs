using CMAReader.Build;
using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader
{
    class Program
    {
        static void Main(string[] args)
        {


            IPatternXML patternXML = new PatternXML();



            //Console.WriteLine("Test {0}", patternXML.GetValidCommodityColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidCommodityHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidCommodityTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidDestinationArbColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidDestinationArbHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidDestinationArbTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidDryRateColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidDryRateHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidDryRateTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidFreetimeColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidFreetimeHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidFreetimeTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidGeneralSurchargeColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidGeneralSurchargeHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidGeneralSurchargeTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidGRIColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidGRIHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidGRITableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidNote4HeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidNote4TableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidOriginArbColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidOriginArbHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidOriginArbTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidOSPFColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidOSPFHeader1Keyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidOSPFTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidPortGroupColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidPortGroupHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidPortGroupTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidReeferRateColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidReeferRateHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidReeferRateTableKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidSheetKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidSpecialRateColumnKeywords.OfType<string>().Count());
            //Console.WriteLine("Test {0}", patternXML.GetValidSpecialRateHeaderKeyword);
            //Console.WriteLine("Test {0}", patternXML.GetValidSpecialRateTableKeyword);



            ContractInfo contractInfo = new ContractInfo
            {
                _AmdId = "TEST",
                _Eff = Convert.ToDateTime("10/20/2018"),
                _Exp = Convert.ToDateTime("10/30/2018"),
                _ContractId = "TEST",
                _Carrier = "TEST"
            };

            using(var prop = new ContractProperties(contractInfo, @"C:\Users\nel\Desktop\REFERENCE FILE\test\ceva cma 16-1453 am44.xlsx"))
            {
                prop.OpenSheets();
                prop.ReadSheets();
            }




            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
