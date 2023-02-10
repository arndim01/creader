using ClosedXML.Excel;
using CMAReader.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Interfaces
{
    public interface IContractSheet
    {
        IXLWorksheet _Ws { get; }
        IPatternXML _Pattern { get; }
        ContractInfo _ContractInfo { get; }
        string _SheetName { get; }
        bool _ValidSheet { get; }
        int _GetTotalRowsUsed { get; }
        int _GetTotalColsUsed { get; }
        int _CurrentLine { get; }
        void NextLine();
        void BackLine();
        void ResetLine();
    }
}
