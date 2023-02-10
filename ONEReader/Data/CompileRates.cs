using ONEReader.Build;
using ONEReader.DataHelper;
using ONEReader.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public class CompileRates<T> : ICompile<T> where T : class
    {
        public DataTable convertedData { get; set; }
        public IItem<T> items { get; set; }

        public IItem<GroupDetails> groupItems { get; set; }
        public IItem<NotesDetails> rateItems { get; set; }

        private int _ID;
        private int _GID;
        private int _TID;
        private List<NotesDetails> rateLine;
        private IContractsheet _contract;
        //private BasicInfo basicInfo;
        private ContractValidity contractValidity;
        private T genericReferece;
        public CompileRates(T value)
        {
            genericReferece = value;
        }
        public CompileRates(IContractsheet contract) 
        {
            _ID = 1;
            _GID = 1;
            _TID = 1;
            _contract = contract;
            convertedData = new DataTable("rates");

            items = new ItemDetails<T>();
            groupItems = new ItemDetails<GroupDetails>();
            rateItems = new ItemDetails<NotesDetails>();

            rateLine = new List<NotesDetails>();
            //rateItems = new ItemDetails<NotesDetails>();
            //basicInfo = new BasicInfo(contract.contractInfo.carrier, contract.contractInfo.eff.convertDateFormat(), contract.contractInfo.exp.convertDateFormat(), contract.contractInfo.amdId, contract.sheetName);
            contractValidity = new ContractValidity(contract.contractInfo.eff.convertDateFormat(), contract.contractInfo.exp.convertDateFormat());
        }
        public void CompileData(IHeader header, List<TableContent> body, FooterContent footer)
        {
            footer.NoteString.getNotes(_contract.pattern, out string generalNotesValue, out string notes, out bool generalNotesExist);

            var commodityInfo = new Commodity((header as HeaderRate).Commodity, (header as HeaderRate).Commodity, "", "");

            var originInfo = new CityInfo((header as HeaderRate).Origin, (header as HeaderRate).OriginVia, In_Point.R_ORG );

            foreach (TableContent bd in body)
            {
                CityInfo destinationInfo = new CityInfo
                {
                    inland_pointer = In_Point.R_DES
                };
                MiscRateInfo miscRateInfo = new MiscRateInfo();
                BaseRateInfo baseRateInfo;
                Notes notesInfo = new Notes();
                Scope scope = new Scope();
                notesInfo.rateLineNotes = notes;

                scope.trade = (header as HeaderRate).Scope;
                scope.bullet = (header as HeaderRate).Bullet;
                miscRateInfo.o_service = originInfo.cityDetail.getService();
                

                //total row to be added
                int totalRows = 1;
                string container_type = "", currency = "";
                List<string> br20 = new List<string>();
                List<string> br40 = new List<string>();
                List<string> br40hc = new List<string>();
                List<string> br45 = new List<string>();
                

                foreach (KeyValuePair<string, string> kvp in bd.CellValue)
                {
                    if (kvp.Key == "destination")
                    {
                        destinationInfo.cityDetail = kvp.Value;
                    }
                    else if (kvp.Key == "cntry")
                    {
                        if( !String.IsNullOrWhiteSpace(kvp.Value))
                        {
                            destinationInfo.cityDetail += " " + kvp.Value;
                        }
                    }
                    else if (kvp.Key == "destinationvia" || kvp.Key == "via")
                    {
                        destinationInfo.cityViaDetail = kvp.Value;
                    }
                    else if (kvp.Key == "cntry1")
                    {
                        if(!String.IsNullOrWhiteSpace(kvp.Value))
                        {
                            destinationInfo.cityViaDetail += " " + kvp.Value;
                        }
                    }
                    else if (kvp.Key == "term")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            miscRateInfo.d_service = kvp.Value.getService2();
                        else
                            miscRateInfo.d_service = "CY(DEFAULT)";
                    }
                    else if (kvp.Key == "type")
                    {
                        miscRateInfo.hazType = kvp.Value;
                        container_type = kvp.Value;
                    }
                    else if (kvp.Key == "cur")
                    {
                        currency = kvp.Value;
                    }
                    else if (kvp.Key == "directcall")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value)) {
                            miscRateInfo.customNotes = "DIRECT CALL:" + kvp.Value;
                        }
                    }
                    else if (kvp.Key == "note")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                        {
                            miscRateInfo.numberNotes = kvp.Value;
                        }
                    }
                    else if (kvp.Key == "20'")
                    {
                        var splitValue = kvp.Value.Split('@');
                        if (totalRows < splitValue.Count()) totalRows = splitValue.Count();
                        br20 = splitValue.ToList();
                    }
                    else if (kvp.Key == "40'")
                    {
                        var splitValue = kvp.Value.Split('@');
                        if (totalRows < splitValue.Count()) totalRows = splitValue.Count();
                        br40 = splitValue.ToList();
                    }
                    else if (kvp.Key == "40hc")
                    {
                        var splitValue = kvp.Value.Split('@');
                        if (totalRows < splitValue.Count()) totalRows = splitValue.Count();
                        br40hc = splitValue.ToList();
                    }
                    else if (kvp.Key == "45'")
                    {
                        var splitValue = kvp.Value.Split('@');
                        if (totalRows < splitValue.Count()) totalRows = splitValue.Count();
                        br45 = splitValue.ToList();
                    }
                }

                for (int i = 0; i < totalRows; i++)
                {
                    baseRateInfo = new BaseRateInfo();
                    if (i <= br20.Count() - 1)
                    {
                        baseRateInfo.BR_20 = br20[i];
                    }
                    else
                        baseRateInfo.BR_20 = "";
                    if (i <= br40.Count() - 1)
                    {
                        baseRateInfo.BR_40 = br40[i];
                    }
                    else
                        baseRateInfo.BR_40 = "";
                    if (i <= br40hc.Count() - 1)
                    {
                        baseRateInfo.BR_40HC = br40hc[i];
                    }
                    else
                        baseRateInfo.BR_40HC = "";
                    if (i <= br45.Count() - 1)
                    {
                        baseRateInfo.BR_45 = br45[i];
                    }
                    else
                        baseRateInfo.BR_45 = "";


                    baseRateInfo.rateType = RateType.RATE;
                    baseRateInfo.containerType = container_type;
                    baseRateInfo.currency = currency;

                    var rateDetails = new RateDetails(_ID, _GID, _TID,  contractValidity, commodityInfo, originInfo, destinationInfo, baseRateInfo, miscRateInfo, notesInfo, scope, bd.RowColorSchemes);

                    items.Add((rateDetails as T));
                    _ID++;
                }
            }

            //TABLE ID CHANGE DIFFERENT ANGLE
            var newRateLine = RemoveNotRatelineNotes(footer.RowColorSchemes);
            var tableNotes = new NotesDetails(_TID, (header as HeaderRate).Origin + "|" + (header as HeaderRate).OriginVia, notes, newRateLine);
            //rateItems.Add(tableNotes);
            rateLine.Add(tableNotes);
            _TID++;
            //GROUP ID
            if (generalNotesExist)
            {
                if (header.CommodityOnchanged)
                {
                    header.CommodityOnchanged = false;
                    footer.RowColorSchemes = RemoveNotGeneralNotes(footer.RowColorSchemes);
                    var groupD = new GroupDetails(_GID, header.CurrentLine, (header as HeaderRate).Commodity, generalNotesValue, (header as HeaderRate).Scope, "RATES", rateLine, footer.RowColorSchemes);
                    groupItems.Add(groupD);
                    //var groupNotes = new NotesDetails(_GID, _TID, generalNotesValue);
                    //groupItems.Add(groupNotes);
                    _GID++;
                    _TID = 1;
                    rateLine = new List<NotesDetails>();
                }
            }
            else
            {
                if (header.ForeHeader == Enum.ForeCastHeader.Commodity || header.ForeHeader == Enum.ForeCastHeader.Scope)
                {
                    footer.RowColorSchemes = RemoveNotGeneralNotes(footer.RowColorSchemes);
                    var groupD = new GroupDetails(_GID, header.CurrentLine, (header as HeaderRate).Commodity, generalNotesValue, (header as HeaderRate).Scope, "RATES", rateLine, footer.RowColorSchemes);
                    groupItems.Add(groupD);
                    //var groupNotes = new NotesDetails(_GID, _TID, generalNotesValue);
                    //groupItems.Add(groupNotes);
                    _GID++;
                    _TID = 1;
                    rateLine = new List<NotesDetails>();
                }
            }
        }
        private List<RowColorScheme> RemoveNotGeneralNotes(List<RowColorScheme> rowColorSchemes) {

            List<RowColorScheme> newSchemes = new List<RowColorScheme>();
            for(int i =0; i < rowColorSchemes.Count; i++)
            {
                if (_contract.pattern.isKeyWordNotesExist(rowColorSchemes[i].TextValue))
                {
                    if( rowColorSchemes.Count - 1 != i)
                    {
                        for(int j = i + 1; j < rowColorSchemes.Count; j++)
                        {
                            newSchemes.Add(rowColorSchemes[j]);
                        }

                        return newSchemes;
                    }
                }
            }
            return newSchemes;
        }
        private List<RowColorScheme> RemoveNotRatelineNotes(List<RowColorScheme> rowColorSchemes)
        {
            List<RowColorScheme> newSchemes = new List<RowColorScheme>();
            for (int i = 0; i < rowColorSchemes.Count; i++)
            {
                if(_contract.pattern.isKeyWordNotesExist(rowColorSchemes[i].TextValue))
                {
                    break;
                }
                else
                {
                    newSchemes.Add(rowColorSchemes[i]);
                }
            }
            return newSchemes;
        }

    }
}
