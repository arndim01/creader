using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ONEReader.Build
{
    public class ContractProperties : IContractProperties, IDisposable
    {
        public ContractProperties(ContractInfo _contractInfo, string path)
        {
            contractInfo = _contractInfo;
            contractPath = path;
            workbook = new XLWorkbook(path); //NEEDED VALIDATION IF CONTRACT OPEN
            sheets = workbook.Worksheets;
            contractSheets = new Contractsheets(this);
            pattern = new PatternXML();
        }


        public XLWorkbook workbook { get; set; }
        public ContractInfo contractInfo { get; set; }
        public IPatternXML pattern { get; set; }
        public string contractPath { get; set; }



        public IContractsheets contractSheets { get; protected set; }
        public IXLWorksheets sheets { get; set; }
        public List<string> sheetList()
        {
            return sheets.Select(s => s.Name).ToList();
        }
        public void Dispose()
        {
            workbook = null;
            contractSheets = null;
            contractInfo = null;
            contractPath = null;
            pattern = null;
        }
    }
}
