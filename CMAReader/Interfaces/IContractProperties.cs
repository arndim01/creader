using ClosedXML.Excel;
using CMAReader.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Interfaces
{
    public interface IContractProperties
    {
        XLWorkbook _Workbook { get; set; }
        ContractInfo _ContractInfo { get; set; }
        string _ContractPath { get; set; }
        IContractSheets _ContractSheets { get; set; }
        IXLWorksheets _Sheets { get; set; }
        IEnumerable<string> GetSheetListName();
        void OpenSheets();
        void ReadSheets();
        void WriteSheets();
    }
}
