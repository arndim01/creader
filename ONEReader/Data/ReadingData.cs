using ClosedXML.Excel;
using ONEReader.Build;
using ONEReader.DataHelper;
using ONEReader.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public static class ReadingData
    {

        public static string GetEntireRow(this IContractsheet contract, int LINE)
        {
            string value = "";
            var columnsUsed = contract.ws.Row(LINE).AsRange().CellsUsed();
            foreach (var column in columnsUsed)
            {
                value += column.Value;
            }
            return value;
        }
        public static void GetHeaderData(this IContractsheet contract, IHeader header, out string newStrStored)
        {
            
            Dictionary<int, string[]> scan = new Dictionary<int, string[]>();
            int maxString = 0;
            int index = 1;
            while (true)
            {
                var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();

                /*
                    Get cellsUsed and separate the escaped value.
                */
                foreach (var column in columnsUsed)
                {
                    string searchString = Convert.ToString(column.Value).removeQuotes();
                    List<string> splitEscapedValue = new List<string>();
                    if (!String.IsNullOrWhiteSpace(searchString))
                        splitEscapedValue = Regex.Split(searchString, "\n").ToList();
                    else
                        splitEscapedValue.Add(searchString);

                    if (splitEscapedValue.Count() > maxString) maxString = splitEscapedValue.Count();
                    scan.Add(index, splitEscapedValue.ToArray());
                    index++;
                }
                /*
                    Combine escaped value.   
                */
                for (int i = 0; i < maxString; i++)
                {
                    string LineString = "";
                    for (int j = 1; j <= scan.Count(); j++)
                    {
                        if (scan[j].Count() - 1 >= i)
                        {
                            if(!String.IsNullOrWhiteSpace(scan[j][i]))
                            {
                                LineString += scan[j][i];
                            }
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(LineString))
                    {
                        header.ExtractHeader(LineString, contract.currentLine);
                    }
                }
                scan.Clear();
                index = 1;
                contract.nextLine();
                newStrStored = contract.GetEntireRow(contract.currentLine);
                
                if (contract.pattern.isTableExist(newStrStored) || contract.currentLine == contract.getTotalRowsUsed) break;
            }
            //Console.WriteLine(newStrStored);
        }
        public static List<TableContent> GetTableData(this IContractsheet contract)
        {
            int headerColumnLocation = contract.currentLine;
            var headerColumnPosition = contract.ws.Row(headerColumnLocation).AsRange().CellsUsed();
            contract.nextLine();
            int starterValueLocation = contract.currentLine;
            int endTableRowLocation = contract.getTableLastRowUsed();
            //List<Dictionary<string, string>> tableData = new List<Dictionary<string, string>>();
            List<TableContent> tableContents = new List<TableContent>();
            while (starterValueLocation <= endTableRowLocation)
            {
                int startedCellPosition = starterValueLocation;
                string strStore = contract.GetEntireRow(starterValueLocation).removeQuotes();
                if (!contract.pattern.isTableExist(strStore) && !strStore.isBlankValue())
                {
                    Dictionary<string, int> tableColumnLocation = contract.IdentifyColumnLocation(headerColumnLocation);
                    //Dictionary<string, string> tableValue = new Dictionary<string, string>();
                    TableContent tableContent = new TableContent();
                    ValueDefinition valueDefinition = new ValueDefinition();
                    int endRowLocation = contract.endRowLocation(starterValueLocation, endTableRowLocation);
                    foreach (KeyValuePair<string, int> kvp in tableColumnLocation)
                    {
                        string combineCellValue = "";
                        int columnPosition = kvp.Value;
                        for (startedCellPosition = starterValueLocation; startedCellPosition <= endRowLocation; startedCellPosition++)
                        {
                            string extractedCellValue = contract.ws.Cell(startedCellPosition, kvp.Value).Value.ToString().customRatesValue();

                            //Analyze Color Result
                            contract.ws.Cell(startedCellPosition, kvp.Value).RichText.ForEach(c =>
                            {

                                RowColorScheme rowColorScheme;

                                switch (c.FontColor.ColorType)
                                {
                                    case XLColorType.Color:
                                        Console.WriteLine("{0} | COLOR: {1}", c.Text, c.FontColor.Color);
                                        //valueDefinition.colorType = XLColorType.Color;
                                        //valueDefinition.colorValue = c.FontColor.Color;
                                        //valueDefinition.textValue = extractedCellValue;
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Color.ToString(),
                                            ColorValue = c.FontColor.Color.Name.ToString(),
                                            TextValue = c.Text,
                                            IdValue = kvp.Key,
                                            ColorValueDefault =  Enum.ColorValueDefault.CUSTOM
                                        };
                                        tableContent.RowColorSchemes.Add(rowColorScheme);
                                        break;
                                    case XLColorType.Indexed:
                                        Console.WriteLine("{0} | INDEXED: {1}", c.Text, c.FontColor.Indexed);
                                        //valueDefinition.colorType = XLColorType.Indexed;
                                        //valueDefinition.indexColor = c.FontColor.Indexed;
                                        //valueDefinition.textValue = extractedCellValue;
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Indexed.ToString(),
                                            IndexColor = c.FontColor.Indexed.ToString(),
                                            TextValue = c.Text,
                                            IdValue = kvp.Key,
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };
                                        tableContent.RowColorSchemes.Add(rowColorScheme);
                                        break;
                                    case XLColorType.Theme:
                                        Console.WriteLine("{0} | THEME: {1}", c.Text, c.FontColor.ThemeColor);
                                        //valueDefinition.colorType = XLColorType.Theme;
                                        //valueDefinition.themeColor = c.FontColor.ThemeColor;
                                        //valueDefinition.textValue = extractedCellValue;
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Theme.ToString(),
                                            ThemeColor = c.FontColor.ThemeColor.ToString(),
                                            TextValue = c.Text,
                                            IdValue = kvp.Key,
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        tableContent.RowColorSchemes.Add(rowColorScheme);

                                        break;
                                    default:
                                        Console.WriteLine("{0} | DEFAULT: {1}", c.Text, "default");
                                        //valueDefinition.colorType = XLColorType.Color;
                                        //valueDefinition.colorValue = Color.Black;
                                        //valueDefinition.textValue = extractedCellValue;

                                        rowColorScheme = new RowColorScheme
                                        {
                                            TextValue = c.Text,
                                            IdValue = kvp.Key,
                                            ColorValueDefault = Enum.ColorValueDefault.DEFAULT
                                        };


                                        break;
                                }
                            });
                            combineCellValue += extractedCellValue;
                        }
                        tableContent.CellValue.Add(kvp.Key, combineCellValue);
                        //tableValue.Add(kvp.Key, combineCellValue);
                    }
                    tableContents.Add(tableContent);
                    //tableData.Add(tableValue);
                    starterValueLocation = startedCellPosition;
                }
                else
                {
                    starterValueLocation++;
                }
            }
            return tableContents;
        }
        public static FooterContent GetNotes(this IContractsheet contract, IHeader header)
        {
            string strStore = "";
            FooterContent footerContent = new FooterContent();
            while (true)
            {
                strStore = contract.GetEntireRow(contract.currentLine);
                
                if (contract.pattern.isTableExist(strStore) || contract.pattern.isMainSearchExist(strStore) || contract.pattern.isHeaderExist(strStore) || contract.pattern.isOriginArbsExist(strStore) || contract.pattern.isKillSearchExist(strStore) || contract.currentLine == contract.getTotalRowsUsed)
                {
                    if( strStore.IsCommodity())
                    {
                        header.ForeHeader = Enum.ForeCastHeader.Commodity;
                    }else if( strStore.IsScope())
                    {
                        header.ForeHeader = Enum.ForeCastHeader.Scope;
                    }else if( strStore.IsOrigin())
                    {
                        header.ForeHeader = Enum.ForeCastHeader.Origin;
                    }
                    break;
                }
                else
                {
                    var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();
                    foreach (var column in columnsUsed)
                    {
                        column.RichText.ForEach(c =>
                        {

                            RowColorScheme rowColorScheme;
                            switch (c.FontColor.ColorType)
                            {
                                case XLColorType.Color:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Color.ToString(),
                                            ColorValue = c.FontColor.Color.Name.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }

                                    break;
                                case XLColorType.Indexed:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Indexed.ToString(),
                                            IndexColor = c.FontColor.Indexed.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }


                                    break;
                                case XLColorType.Theme:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Theme.ToString(),
                                            ThemeColor = c.FontColor.ThemeColor.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }

                                    break;
                                default:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.DEFAULT
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }
                                    break;
                            }
                        });
                    }


                    contract.nextLine();
                }
                if (!String.IsNullOrWhiteSpace(strStore) && !strStore.isBlankValue())
                {
                    footerContent.NoteString = footerContent.NoteString + strStore.removeQuotes().customRatesValue() + "@";

                }
            }
            //Console.WriteLine(notes);
            return footerContent;
        }
        public static FooterContent GetArbNotes(this IContractsheet contract, IHeader header)
        {
            string strStore = "";
            FooterContent footerContent = new FooterContent();
            while (true)
            {
                strStore = contract.GetEntireRow(contract.currentLine);
                if ( contract.pattern.isTableExist(strStore) || contract.pattern.isArbsHeaderExist(strStore) ||  contract.pattern.isDestinationArbsExist(strStore) || contract.pattern.isKillSearchExist(strStore))
                {
                    if (strStore.IsApplicableOver())
                    {
                        header.ForeHeader = Enum.ForeCastHeader.Over;
                    }
                    break;
                }
                else
                {
                    var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();
                    foreach (var column in columnsUsed)
                    {
                        column.RichText.ForEach(c =>
                        {
                            RowColorScheme rowColorScheme;
                            switch (c.FontColor.ColorType)
                            {
                                case XLColorType.Color:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Color.ToString(),
                                            ColorValue = c.FontColor.Color.Name.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }

                                    break;
                                case XLColorType.Indexed:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Indexed.ToString(),
                                            IndexColor = c.FontColor.Indexed.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };

                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }


                                    break;
                                case XLColorType.Theme:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            ColorType = XLColorType.Theme.ToString(),
                                            ThemeColor = c.FontColor.ThemeColor.ToString(),
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.CUSTOM
                                        };
                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }

                                    break;
                                default:
                                    if (!String.IsNullOrWhiteSpace(c.Text))
                                    {
                                        rowColorScheme = new RowColorScheme
                                        {
                                            TextValue = c.Text.removeQuotes().customRatesValue(),
                                            ColorValueDefault = Enum.ColorValueDefault.DEFAULT
                                        };
                                        footerContent.RowColorSchemes.Add(rowColorScheme);
                                    }
                                    break;
                            }
                        });
                    }

                    contract.nextLine();
                }




                if(!String.IsNullOrWhiteSpace(strStore) && !strStore.isBlankValue())
                {
                    footerContent.NoteString = footerContent.NoteString + "@" + strStore.removeQuotes().customRatesValue();
                   
                }
            }
            return footerContent;
        }
        private static Dictionary<string, int> IdentifyColumnLocation(this IContractsheet contract, int headerColumnLocation)
        {
            Dictionary<string, int> set = new Dictionary<string, int>();
            var headerColumnPosition = contract.ws.Row(headerColumnLocation).AsRange().CellsUsed();
            foreach(var column in headerColumnPosition)
            {
                int pos = column.Address.ColumnNumber;
                string key = column.Value.ToString().ToLowerInvariant().UnescapeValue().Replace(" ", "");
                if(!set.ContainsKey(key))
                {
                    set.Add(key, pos);
                }
                else
                {
                    set.Add(key + "1", pos);
                }

            }

            return set;
        }
        private static int getTableLastRowUsed(this IContractsheet contract)
        {
            int endTableRowLocation = 0;
            while(contract.currentLine <= contract.getTotalRowsUsed)
            {

                string strStore = contract.GetEntireRow(contract.currentLine);
                if (contract.pattern.isHeaderExist(strStore) || contract.pattern.isMainSearchExist(strStore)) break;
                if (strStore.UnescapeValue().Replace(" ", "") != "" && !contract.isLeftBorderExist() && strStore.UnescapeValue().Replace(" ", "").ToUpperInvariant() != "BLANK") break;
                
                var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();
                
                foreach(var column in columnsUsed)
                {
                    if (column.Style.Border.LeftBorder != XLBorderStyleValues.None && column.Style.Border.BottomBorder != XLBorderStyleValues.None)
                    {
                        endTableRowLocation = contract.currentLine;
                        break;
                    }
                }
                contract.nextLine();
            }
            return endTableRowLocation;
        }
        private static bool isLeftBorderExist(this IContractsheet contract)
        {
            var columnsUsed = contract.ws.Row(contract.currentLine).AsRange().CellsUsed();
            foreach(var column in columnsUsed)
            {
                if( column.Style.Border.LeftBorder != XLBorderStyleValues.None)
                {
                    return true;
                }
            }
            return false;
        }
        private static int endRowLocation(this IContractsheet contract,int starterCellLocation, int lastTableLocation)
        {
            while (starterCellLocation <= lastTableLocation)
            {
                var columnsUsed = contract.ws.Row(starterCellLocation).AsRange().CellsUsed();
                foreach (var column in columnsUsed)
                {
                    if( column.Style.Border.BottomBorder != XLBorderStyleValues.None)
                    {
                        return starterCellLocation;
                    }
                }
                starterCellLocation++;
            }
            return 0;
        }

    }
}
