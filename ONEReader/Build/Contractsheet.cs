using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    internal class Contractsheet : IContractsheet
    {

        internal Contractsheet(string sheetName, ContractProperties contractProperties)
        {
            this.sheetName = sheetName;
            ws = contractProperties.workbook.Worksheet(sheetName);
            readline = 1;
            pattern = contractProperties.pattern;
            contractInfo = contractProperties.contractInfo;
        }
        public IPatternXML pattern { get; }
        public IXLWorksheet ws { get;}
        public string sheetName { get; }
        public bool validSheet { get; }
        public ContractInfo contractInfo {get;}
        public int getTotalColsUsed
        {
            get
            {
                return ws.RangeUsed().LastColumnUsed().ColumnNumber();
            }
        }
        public int getTotalRowsUsed
        {
            get
            {
                return ws.LastRowUsed().RowNumber();
            }
        }
        //Read next line
        public void nextLine()
        {
            readline++;
        }
        public void backLine()
        {
            readline--;
        }
        public int readline { get; private set; }
        public int currentLine
        {
            get
            {
                return readline;
            }
        }
    }
}
