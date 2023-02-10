using ClosedXML.Excel;
using CMAReader.Data;
using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Build
{
    public class ContractProperties : IContractProperties, IDisposable
    {
        public ContractProperties(ContractInfo ContractInfo, string ContractPath)
        {
            _ContractInfo = ContractInfo;
            _ContractPath = ContractPath;
        }

        public XLWorkbook _Workbook { get; set; }
        public ContractInfo _ContractInfo { get; set; }
        public string _ContractPath { get; set; }
        public IContractSheets _ContractSheets { get; set; }
        public IXLWorksheets _Sheets { get; set; }



        public IEnumerable<string> GetSheetListName()
        {
            return _Sheets.Select(s => s.Name).ToList();
        }

        public void OpenSheets()
        {
            _Workbook = new XLWorkbook(_ContractPath);
            _Sheets = _Workbook.Worksheets;
            _ContractSheets = new ContractSheets(this);
            foreach (IXLWorksheet sheet in _Workbook.Worksheets)
            {
                _ContractSheets.Add(sheet.Name);
            }
        }

        public void ReadSheets()
        {
            Assemble assemble = new Assemble( _ContractSheets);
            assemble.ReadData();
        }

        public void WriteSheets()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _Workbook = null;
            _ContractSheets = null;
            _ContractInfo = null;
            _ContractPath = null;
        }
    }
}
