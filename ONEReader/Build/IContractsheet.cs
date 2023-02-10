using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    public enum PatternIdentity
    {
        NULL = 0,
        Header = 1,
        Body = 2,
        Footer = 3
    }
    public enum DataIdentity
    {
        NULL = 0,
        NORMAL = 1,
        DELETE = 2,
        NEW = 3
    }
    public interface IContractsheet
    {
        IXLWorksheet ws { get; }
        IPatternXML pattern { get; }
        ContractInfo contractInfo { get; }
        string sheetName { get; }
        bool validSheet { get;  }
        int getTotalRowsUsed { get; }
        int getTotalColsUsed { get; }
        int currentLine { get; }
        void nextLine();
        void backLine();
    }
}
