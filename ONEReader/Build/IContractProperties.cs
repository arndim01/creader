using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    public interface IContractProperties
    {
        XLWorkbook workbook { get; set; }
        ContractInfo contractInfo { get; set; }
        IPatternXML pattern { get; set; }
        string contractPath { get; set; }
    }
}
