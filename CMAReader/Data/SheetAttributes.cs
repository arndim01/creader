using ClosedXML.Excel;
using CMAReader.Build;
using CMAReader.Helper;
using CMAReader.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Data
{

    public class SheetObject
    {
        public bool HeaderExist { get; set; } = false;
        public bool TableExist { get; set; } = false;
        public DataTable DataTable { get; set; } = new DataTable();
        public Dictionary<SheetColumn, int> ColumnPosition { get; set; } = new Dictionary<SheetColumn, int>();
        public IEnumerable ColumnsKeyWord { get; set; }
        public int HeaderTableLocation { get; set; } = 0;
        public int StartLocation { get; set; } = 0;
        public int StartColumnLocation { get; set; } = 0;
        public int EndLocation { get; set; } = 0;
    }
    public class SheetSurchargeObject
    {
        public bool HeaderExist { get; set; } = false;
        public bool TableExist { get; set; } = false;
        public DataTable DataTable { get; set; } = new DataTable();
        public Dictionary<SheetColumn, int> ColumnPosition { get; set; } = new Dictionary<SheetColumn, int>();
        public IEnumerable ColumnsKeyWord { get; set; }
        public int HeaderTableLocation { get; set; } = 0;
        public int StartLocation { get; set; } = 0;
        public int StartColumnLocation { get; set; } = 0;
        public int EndLocation { get; set; } = 0;
    }
    public class SheetColumn
    {
        public string Name { get; set; }
        public ColumnType ColumnType { get; set; }
    }
    public enum ColumnType
    {
        DEFAULT,
        SURCHARGE
    }
    public class SheetAttributes
    {

        private readonly IPatternXML _PatternXML;
        private readonly IContractSheet _ContractSheet;
        public SheetAttributes(IContractSheet ContractSheet)
        {
            _PatternXML = new PatternXML();
            _ContractSheet = ContractSheet;
        }

        public SheetObject CommodityObject { get; set; } = new SheetObject();
        public SheetObject PortCodeObject { get; set; } = new SheetObject();
        public SheetObject RateObject { get; set; } = new SheetObject();
        public SheetObject RERateObject { get; set; } = new SheetObject();
        public SheetObject SPRateObject { get; set; } = new SheetObject();
        public SheetObject OarbObject { get; set; } = new SheetObject();
        public SheetObject DarbObject { get; set; } = new SheetObject();
        public SheetObject SurchargeObject { get; set; } = new SheetObject();
        public SheetObject GriObject { get; set; } = new SheetObject();
        public SheetObject OSPFObject { get; set; } = new SheetObject();
        public SheetObject NoteObject { get; set; } = new SheetObject();
        public SheetObject FreeTimeObject { get; set; } = new SheetObject();
        

        private void CaptureColumnPosition(SheetObject SheetObject)
        {
            int TableRowLocation = SheetObject.HeaderTableLocation;
            foreach(var columnField in SheetObject.ColumnsKeyWord)
            {
                SheetObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));

            }
            var CellsUsed = _ContractSheet._Ws.Row(TableRowLocation).AsRange().CellsUsed();
            foreach(var cell in CellsUsed)
            {
                if(SheetObject.ColumnsKeyWord.OfType<string>().Any(cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString().Equals))
                {
                    var columnSheet = new SheetColumn
                    {
                        Name = cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString(),
                        ColumnType = ColumnType.DEFAULT
                    };


                    SheetObject.ColumnPosition.Add(columnSheet, cell.Address.ColumnNumber);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        if (!SheetObject.DataTable.Columns.Contains(cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString()))
                        {

                            var columnSheet = new SheetColumn
                            {
                                Name = cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString(),
                                ColumnType = ColumnType.SURCHARGE
                            };

                            SheetObject.DataTable.Columns.Add(new DataColumn(cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString()));
                            SheetObject.ColumnPosition.Add(columnSheet, cell.Address.ColumnNumber);
                        }
                    }
                }
            }

        }

        private void CaptureTableData(SheetObject SheetObject)
        {
            int StartRowPosition = SheetObject.StartLocation;
            for(; StartRowPosition <= SheetObject.EndLocation; StartRowPosition++)
            {
                SheetObject.DataTable.Rows.Add();
                foreach (var position in SheetObject.ColumnPosition)
                {
                    var CellValue = _ContractSheet._Ws.Cell(StartRowPosition, position.Value);

                    if (!String.IsNullOrWhiteSpace(CellValue.Value.ToString()))
                    {
                        SheetObject.DataTable.Rows[SheetObject.DataTable.Rows.Count - 1][position.Key.Name] = CellValue.Value.ToString();
                    }
                }
            }
        }

        private void CastToRemoveEmptyRows(SheetObject SheetObject)
        {

            List<int> rowIndexesToBeDeleted = new List<int>();
            int indexCount = 0;
            foreach (var row in SheetObject.DataTable.Rows)
            {
                var r = (DataRow)row;
                int emptyCount = 0;
                int itemArrayCount = r.ItemArray.Length;
                foreach (var i in r.ItemArray) if (string.IsNullOrWhiteSpace(i.ToString())) emptyCount++;

                if (emptyCount == itemArrayCount) rowIndexesToBeDeleted.Add(indexCount);

                indexCount++;
            }

            int count = 0;
            foreach (var i in rowIndexesToBeDeleted)
            {
                SheetObject.DataTable.Rows.RemoveAt(i - count);
                count++;
            }
            SheetObject.DataTable.AcceptChanges();

        }

        private int FindEndPointTable(SheetObject sheetObject)
        {
            int StartRow = sheetObject.HeaderTableLocation;
            int StartCol = sheetObject.StartColumnLocation;
            int LastRow = _ContractSheet._GetTotalRowsUsed;
            int LastCol = _ContractSheet._GetTotalColsUsed;
            bool FoundEmptyBorder = false;
            for (; StartRow <= LastRow; StartRow++)
            {
                for (int IncCol = StartCol; IncCol <= LastCol; IncCol++)
                {
                    var cell = _ContractSheet._Ws.Cell(StartRow, IncCol);
                    if (cell.Style.Border.LeftBorder != XLBorderStyleValues.None) break;
                    else if (cell.Style.Border.LeftBorder == XLBorderStyleValues.None)
                    {
                        FoundEmptyBorder = true;
                        break;
                    }
                }
                if (FoundEmptyBorder) break;
            }
            return StartRow - 1;
        }

        protected void VALIDATE_COMMODITY_HEADER()
        {
            var CommodityHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidCommodityHeaderKeyword)).FirstOrDefault();
            if (CommodityHeader != null)
            {
                var CommodityTable = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidCommodityTableKeyword)).FirstOrDefault();
                if (CommodityTable != null)
                {
                    CommodityObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidCommodityColumnKeywords;
                    CommodityObject.StartLocation = CommodityTable.RowNumber() + 1;
                    CommodityObject.HeaderTableLocation = CommodityTable.RowNumber();
                    CommodityObject.StartColumnLocation = CommodityTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    CommodityObject.EndLocation = FindEndPointTable(CommodityObject);
                    CaptureColumnPosition(CommodityObject);
                    CaptureTableData(CommodityObject);
                    CastToRemoveEmptyRows(CommodityObject);

                    PrintDataTable(CommodityObject);

                }
            }
        }

        protected void VALIDATE_PORTCODE_HEADER()
        {
            var PortCodeHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidPortGroupHeaderKeyword)).FirstOrDefault();
            if(PortCodeHeader != null)
            {
                var PortCodeTable = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidPortGroupTableKeyword)).FirstOrDefault();
                if(PortCodeTable != null)
                {
                    PortCodeObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidPortGroupColumnKeywords;
                    PortCodeObject.StartLocation = PortCodeTable.RowNumber() + 1;
                    PortCodeObject.HeaderTableLocation = PortCodeTable.RowNumber();
                    PortCodeObject.StartColumnLocation = PortCodeTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    PortCodeObject.EndLocation = FindEndPointTable(PortCodeObject);
                    CaptureColumnPosition(PortCodeObject);
                    CaptureTableData(PortCodeObject);
                    CastToRemoveEmptyRows(PortCodeObject);

                    PrintDataTable(PortCodeObject);
                }
            }
        }
     

        protected void VALIDATE_DRYRATES_HEADER()
        {
            var RateHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidDryRateHeaderKeyword)).FirstOrDefault();
            if(RateHeader != null)
            {
                var RateTable = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidDryRateTableKeyword)).FirstOrDefault();
                if (RateTable != null)
                {
                    RateObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidDryRateColumnKeywords;
                    RateObject.StartLocation = RateTable.RowNumber() + 2;
                    RateObject.HeaderTableLocation = RateTable.RowNumber();
                    RateObject.StartColumnLocation = RateTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    RateObject.EndLocation = FindEndPointTable(RateObject);
                    CaptureColumnPosition(RateObject);
                    CaptureTableData(RateObject);
                    CastToRemoveEmptyRows(RateObject);

                    //PrintDataTable(RateObject);

                }
            }
        }

        private void PrintDataTable(SheetObject SheetObject)
        {
            foreach(DataRow row in SheetObject.DataTable.Rows)
            {
                foreach(DataColumn col in SheetObject.DataTable.Columns)
                {
                    Console.WriteLine("Column Name: {0}, Value: {1}", col.ColumnName, row[col].ToString());
                }
            }
        }


        protected void VALIDATE_REEFERRATES_HEADER()
        {
            var RERateHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidReeferRateHeaderKeyword)).FirstOrDefault();
            if(RERateHeader != null)
            {

                var locateRate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > RateObject.EndLocation);


                var RERateTable = locateRate.Where(row =>  row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidReeferRateTableKeyword)).FirstOrDefault();
                if(RERateTable != null)
                {
                    RERateObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidReeferRateColumnKeywords;
                    RERateObject.StartLocation = RERateTable.RowNumber() + 2;
                    RERateObject.HeaderTableLocation = RERateTable.RowNumber();
                    RERateObject.StartColumnLocation = RERateTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    RERateObject.EndLocation = FindEndPointTable(RERateObject);
                    CaptureColumnPosition(RERateObject);
                    CaptureTableData(RERateObject);
                    CastToRemoveEmptyRows(RERateObject);

                    //PrintDataTable(RERateObject);
                }
            }
        }



        protected void VALIDATE_SPECIALRATES_HEADER()
        {
            var SPRateHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidSpecialRateHeaderKeyword)).FirstOrDefault();
            if(SPRateHeader != null)
            {
                var locateRate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > RERateObject.EndLocation);
                var SPRateTable = locateRate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidSpecialRateTableKeyword)).FirstOrDefault();
                if(SPRateTable != null)
                {
                    SPRateObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidSpecialRateColumnKeywords;
                    SPRateObject.StartLocation = SPRateTable.RowNumber() + 2;
                    SPRateObject.HeaderTableLocation = SPRateTable.RowNumber();
                    SPRateObject.StartColumnLocation = SPRateTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    SPRateObject.EndLocation = FindEndPointTable(SPRateObject);
                    CaptureColumnPosition(SPRateObject);
                    CaptureTableData(SPRateObject);
                    CastToRemoveEmptyRows(SPRateObject);

                    //PrintDataTable(SPRateObject);
                }
            }
        }


        protected void VALIDATE_OARBS_HEADER()
        {
            var OarbHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidOriginArbHeaderKeyword)).FirstOrDefault();
            if(OarbHeader != null)
            {
                var OarbTable = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidOriginArbTableKeyword)).FirstOrDefault();
                if(OarbTable != null)
                {
                    OarbObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidOriginArbColumnKeywords;
                    OarbObject.StartLocation = OarbTable.RowNumber() + 1;
                    OarbObject.HeaderTableLocation = OarbTable.RowNumber();
                    OarbObject.StartColumnLocation = OarbTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    OarbObject.EndLocation = FindEndPointTable(OarbObject);
                    CaptureColumnPosition(OarbObject);
                    CaptureTableData(OarbObject);
                    CastToRemoveEmptyRows(OarbObject);
                }
            }
        }


        protected void VALIDATE_DARBS_HEADER()
        {
            var DarbHeader = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidDestinationArbHeaderKeyword)).FirstOrDefault();
            if(DarbHeader != null)
            {
                var DarbTable = _ContractSheet._Ws.RowsUsed(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidDestinationArbTableKeyword)).FirstOrDefault();
                if(DarbTable != null)
                {
                    DarbObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidDestinationArbColumnKeywords;
                    DarbObject.StartLocation = DarbTable.RowNumber() + 1;
                    DarbObject.HeaderTableLocation = DarbTable.RowNumber();
                    DarbObject.StartColumnLocation = DarbTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    DarbObject.EndLocation = FindEndPointTable(DarbObject);
                    CaptureColumnPosition(DarbObject);
                    CaptureTableData(DarbObject);
                    CastToRemoveEmptyRows(DarbObject);

                }
            }
        }


        protected void VALIDATE_GENERAL_SURCHARGE_HEADER()
        {

            var locateHeader = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > DarbObject.EndLocation);

            var SurchargeHeader = locateHeader.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidGeneralSurchargeHeaderKeyword)).FirstOrDefault();
            if(SurchargeHeader != null)
            {
                var locateRate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > DarbObject.EndLocation);

                var SurchargeTable = locateRate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidGeneralSurchargeTableKeyword)).FirstOrDefault();
                if(SurchargeTable != null)
                {
                    SurchargeObject.ColumnsKeyWord = _ContractSheet._Pattern.GetValidGeneralSurchargeColumnKeywords;
                    SurchargeObject.StartLocation = SurchargeHeader.RowNumber() + 3;
                    SurchargeObject.HeaderTableLocation = SurchargeHeader.RowNumber();
                    SurchargeObject.StartColumnLocation = LocateColumnLocation(SurchargeObject, _ContractSheet._Pattern.GetValidGeneralSurchargeSpecialWord);
                    SurchargeObject.EndLocation = FindEndPointTable(SurchargeObject) - 2;


                    

                }
            }
        }

        private void CreateSurcharge(SheetObject SheetObject)
        {
            int StartRow = SheetObject.StartLocation;
            int EndCol = SheetObject.StartColumnLocation;
            Dictionary<string, string> Position = new Dictionary<string, string>();
            for (int StartCol = 1; StartCol < EndCol; StartCol++)
            {
                var Cell = _ContractSheet._Ws.Cell(StartRow, StartCol);
                if(!String.IsNullOrWhiteSpace(Cell.Value.ToString()))
                {
                    Position.Add(StartCol.ToString(), "surcharge" + StartCol.ToString());

                    var sheetColumn = new SheetColumn
                    {
                        Name = "surcharge" + StartCol.ToString(),
                        ColumnType = ColumnType.DEFAULT
                    };
                    SheetObject.DataTable.Columns.Add(new DataColumn(sheetColumn.Name));
                    SheetObject.ColumnPosition.Add(sheetColumn, StartCol);
                }
            }
            int StartHead = SheetObject.HeaderTableLocation;
            int SCol = SheetObject.StartColumnLocation;
            for (; SCol <= EndCol; SCol++)
            {
                var Cell = _ContractSheet._Ws.Cell(StartHead, SCol);
                var sheetColumn = new SheetColumn
                {
                    Name = Cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString(),
                    ColumnType = ColumnType.DEFAULT
                };
                SheetObject.ColumnPosition.Add(sheetColumn, SCol);
            }
            foreach (var columnField in SheetObject.ColumnsKeyWord)
            {
                SheetObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));
            }

            int SRow = SheetObject.StartLocation;
            for(; SRow <= SheetObject.EndLocation; SRow++)
            {
                var CellsUsed = _ContractSheet._Ws.Row(SRow).AsRange().CellsUsed();
                SheetObject.DataTable.Rows.Add();
                foreach (var cell in CellsUsed)
                {
                    var Key = SheetObject.ColumnPosition.FirstOrDefault(x => x.Value == cell.Address.ColumnNumber).Key;

                    SheetObject.DataTable.Rows[SheetObject.DataTable.Rows.Count - 1][Key.Name] = cell.Value.ToString();
                }
            }
        }

        private int LocateColumnLocation(SheetObject SheetObject, string searchValue)
        {
            var CellsUsed = _ContractSheet._Ws.Row(SheetObject.HeaderTableLocation).AsRange().CellsUsed();
            
            foreach(var cell in CellsUsed)
            {
                if( cell.Value.ToString().ToLowerInvariant().RemoveUnnecessaryString().Contains(searchValue))
                {
                    return cell.Address.ColumnNumber;
                }
            }
            return -1;
        }

        protected void VALIDATE_GRI_HEADER()
        {
            var locate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > SurchargeObject.EndLocation);
            var GriHeader = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidGRIHeaderKeyword)).FirstOrDefault();
            if(GriHeader != null)
            {
                var GriTable = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidGRITableKeyword)).FirstOrDefault();
                if(GriTable != null)
                {
                    foreach(var columnField in _ContractSheet._Pattern.GetValidGRIColumnKeywords)
                    {
                        GriObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));
                    }
                    GriObject.StartLocation = GriTable.RowNumber();
                    GriObject.StartColumnLocation = GriTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    GriObject.EndLocation = FindEndPointTable(GriObject);

                    Console.WriteLine("GRI Rate Search - SheetName: {0}, StartRow: {1}, EndRow: {2}", _ContractSheet._SheetName, GriObject.StartLocation, GriObject.EndLocation);
                }
            }

        }


        protected void VALIDATE_OSPF_HEADER()
        {
            var locate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > GriObject.EndLocation);
            var OSPFHeader = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidOSPFHeader1Keyword)).FirstOrDefault();
            if(OSPFHeader != null)
            {
                var OSPFTable = locate.Where(row => _ContractSheet._Pattern.GetValidOSPFTableKeyword.Contains(row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString())).FirstOrDefault();
                if(OSPFTable != null)
                {
                    foreach(var columnField in _ContractSheet._Pattern.GetValidOSPFColumnKeywords)
                    {
                        OSPFObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));
                    }
                    OSPFObject.StartLocation = OSPFTable.RowNumber();
                    OSPFObject.StartColumnLocation = OSPFTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    OSPFObject.EndLocation = FindEndPointTable(OSPFObject);

                    Console.WriteLine("OSPF Rate Search - SheetName: {0}, StartRow: {1}, EndRow: {2}", _ContractSheet._SheetName, OSPFObject.StartLocation, OSPFObject.EndLocation);
                }
            }
        }
        
        protected void VALIDATE_NOTES_HEADER()
        {
            var locate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > OSPFObject.EndLocation);
            var NotesHeader = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidNote4HeaderKeyword)).FirstOrDefault();
            if(NotesHeader != null)
            {
                var NotesTable = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidNote4TableKeyword)).FirstOrDefault();
                if(NotesTable != null)
                {
                    foreach(var columnField in _ContractSheet._Pattern.GetValidNote4ColumnKeywords)
                    {
                        NoteObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));
                    }
                    NoteObject.StartLocation = NotesTable.RowNumber();
                    NoteObject.StartColumnLocation = NotesTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    NoteObject.EndLocation = FindEndPointTable(NoteObject);

                    Console.WriteLine("Note Rate Search - SheetName: {0}, StartRow: {1}, EndRow: {2}", _ContractSheet._SheetName, NoteObject.StartLocation, NoteObject.EndLocation);
                }
            }
        }


        protected void VALIDATE_FREETIME_HEADER()
        {
            var locate = _ContractSheet._Ws.RowsUsed(row => row.RowNumber() > NoteObject.EndLocation);
            var FreeTimeHeader = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidFreetimeHeaderKeyword)).FirstOrDefault();
            if(FreeTimeHeader != null)
            {
                var FreeTimeTable = locate.Where(row => row.GetEntireStringInRow().ToLowerInvariant().RemoveUnnecessaryString().Contains(_ContractSheet._Pattern.GetValidFreetimeTableKeyword)).FirstOrDefault();
                if(FreeTimeTable != null)
                {
                    foreach(var columnField in _ContractSheet._Pattern.GetValidFreetimeColumnKeywords)
                    {
                        FreeTimeObject.DataTable.Columns.Add(new DataColumn(columnField.ToString()));
                    }
                    FreeTimeObject.StartLocation = FreeTimeTable.RowNumber();
                    FreeTimeObject.StartColumnLocation = FreeTimeTable.CellsUsed().FirstOrDefault().Address.ColumnNumber;
                    FreeTimeObject.EndLocation = FindEndPointTable(FreeTimeObject);

                    Console.WriteLine("Freetime Rate Search - SheetName: {0}, StartRow: {1}, EndRow: {2}", _ContractSheet._SheetName, FreeTimeObject.StartLocation, FreeTimeObject.EndLocation);
                }
            }
        }

        protected bool Valid()
        {
            return false;
        }


        

    }
}
