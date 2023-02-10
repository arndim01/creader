using ClosedXML.Excel;
using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Build
{
    public class ContractSheet : IContractSheet
    {
        private int _Readline { get; set; }
        public ContractSheet(string SheetName, ContractProperties ContractProperties)
        {
            _SheetName = SheetName;
            _Ws = ContractProperties._Workbook.Worksheet(SheetName);
            _Readline = 1;
            _Pattern = new PatternXML();
            _ContractInfo = ContractProperties._ContractInfo;
        }
        public IXLWorksheet _Ws { get; }

       public IPatternXML _Pattern { get; }

        public ContractInfo _ContractInfo { get; }

        public string _SheetName { get; }

        public bool _ValidSheet { get; }
        

        public int _GetTotalRowsUsed {
            get
            {
                return _Ws.LastRowUsed().RowNumber();
            }
        }

        public int _GetTotalColsUsed {
            get
            {
                return _Ws.RangeUsed().LastColumnUsed().ColumnNumber();
            }
        }

        public int _CurrentLine
        {
            get
            {
                return _Readline;
            }
        }

        public void BackLine()
        {
            _Readline--;
        }

        public void NextLine()
        {
            _Readline++;
        }

        public void ResetLine()
        {
            _Readline = 1;
        }
    }
}
