using ClosedXML.Excel;
using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Data
{
    public static class ReadData
    {
        public static string GetEntireStringInRow(this IContractSheet ContractSheet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var cellsUsed = ContractSheet._Ws.Row(ContractSheet._CurrentLine).AsRange().CellsUsed();
            foreach(var cell in cellsUsed)
            {
                if( cell.HasFormula)
                {
                    if (!String.IsNullOrWhiteSpace(cell.ValueCached.ToString()))
                    {
                        stringBuilder.Append(cell.ValueCached.ToString());
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        stringBuilder.Append(cell.Value.ToString());
                    }
                }
            }
            return stringBuilder.ToString();
        }
        public static string GetEntireStringInRow(this IXLRow IXLRow)
        {
            var CellUsed = IXLRow.CellsUsed();
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var cell in CellUsed)
            {
                if(cell.HasFormula)
                {
                    if (!String.IsNullOrWhiteSpace(cell.ValueCached.ToString()))
                    {
                        stringBuilder.Append(cell.ValueCached.ToString());
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        stringBuilder.Append(cell.Value.ToString());
                    }
                }
            }
            return stringBuilder.ToString();
        }
        public static int GetLastTableRowNumber(this IContractSheet ContractSheet)
        {
            int CurrentLine = ContractSheet._CurrentLine;
            int endTableRowLocation = 0;
            for (; CurrentLine <= ContractSheet._GetTotalRowsUsed; CurrentLine++)
            {

                //string strStore = ContractSheet.GetEntireStringInRow(ContractSheet.currentLine);
                //if (contract.pattern.isHeaderExist(strStore) || contract.pattern.isMainSearchExist(strStore)) break;
                //if (strStore.UnescapeValue().Replace(" ", "") != "" && !contract.isLeftBorderExist() && strStore.UnescapeValue().Replace(" ", "").ToUpperInvariant() != "BLANK") break;

                //var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();

                //foreach (var column in columnsUsed)
                //{
                //    if (column.Style.Border.LeftBorder != XLBorderStyleValues.None && column.Style.Border.BottomBorder != XLBorderStyleValues.None)
                //    {
                //        endTableRowLocation = contract.currentLine;
                //        break;
                //    }
                //}
                //contract.nextLine();
            }
            return endTableRowLocation;
        }

    }
}
