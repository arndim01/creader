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
    public class CompileArbs<T> : ICompile<T> where T : class
    {
        public DataTable convertedData { get; set; }

        public IItem<T> items { get; set; }

        public IItem<GroupDetails> groupItems { get; set; }

        //public IItem<NotesDetails> rateItems { get; set; }
        private int _ID;
        private int _GID;
        private int _TID;
        
        private List<int> _IDS_LIST;
        private IContractsheet _contract;
        //private BasicInfo basicInfo;
        private ContractValidity contractValidity;
        public CompileArbs(IContractsheet contract)
        {
            _ID = 1;
            _GID = 1;
            _TID = 1;
            _IDS_LIST = new List<int>();
            convertedData = new DataTable("arbs");
            _contract = contract;
            items = new ItemDetails<T>();
            groupItems = new ItemDetails<GroupDetails>();
            //rateItems = new ItemDetails<NotesDetails>();
            //basicInfo = new BasicInfo(contract.contractInfo.carrier, contract.contractInfo.eff.convertDateFormat(), contract.contractInfo.exp.convertDateFormat(), contract.contractInfo.amdId, contract.sheetName);
            contractValidity = new ContractValidity(contract.contractInfo.eff.convertDateFormat(), contract.contractInfo.exp.convertDateFormat());
        }
        public void CompileData(IHeader header, List<TableContent> body, FooterContent footer)
        {
            foreach (TableContent bd in body)
            {
                BaseRateInfo baseRateInfo = new BaseRateInfo();
                MiscRateInfo miscRateInfo = new MiscRateInfo();
                CityInfo cityInfo = new CityInfo();
                Notes notesInfo = new Notes();
                Scope scope = new Scope();
                notesInfo.rateLineNotes = footer.NoteString;
                string special_notes = "";

                if ((header as HeaderArb).ArbsView == RateType.OARB) cityInfo.inland_pointer = In_Point.A_ORG;
                else cityInfo.inland_pointer = In_Point.A_DES;

                var inlandDetails = new InlandDetail(contractValidity);

                foreach (KeyValuePair<string, string> kvp in bd.CellValue)
                {
                    if (kvp.Key == "point")
                    {
                        cityInfo.cityDetail = kvp.Value;
                    }
                    else if (kvp.Key == "cntry")
                    {
                        cityInfo.cityDetail += " " + kvp.Value;
                    }
                    else if (kvp.Key == "term")
                    {
                        if( header.Type == RateType.OARB)
                        {
                            if (!String.IsNullOrWhiteSpace(kvp.Value))
                                miscRateInfo.o_service = kvp.Value.getService2();
                            else
                                miscRateInfo.o_service = "CY(DEFAULT)";
                        }
                        else if( header.Type == RateType.DARB)
                        {
                            if (!String.IsNullOrWhiteSpace(kvp.Value))
                                miscRateInfo.d_service = kvp.Value.getService2();
                            else
                                miscRateInfo.d_service = "CY(DEFAULT)";
                        }
                    }
                    else if (kvp.Key == "via")
                    {
                        if( !String.IsNullOrWhiteSpace(kvp.Value) )
                            special_notes = kvp.Value;

                    }
                    else if (kvp.Key == "cntry1")
                    {
                        if( !String.IsNullOrWhiteSpace(kvp.Value))
                            special_notes += " " + kvp.Value;
                    }
                    else if (kvp.Key == "mode")
                    {
                        miscRateInfo.mode = kvp.Value;
                    }
                    else if (kvp.Key == "type")
                    {
                        miscRateInfo.hazType = kvp.Value;
                        baseRateInfo.containerType = kvp.Value;
                    }
                    else if (kvp.Key == "cur")
                    {
                        baseRateInfo.currency = kvp.Value;
                    }
                    else if (kvp.Key == "box")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                        {
                            baseRateInfo.BR_20 = kvp.Value;
                            baseRateInfo.BR_40 = kvp.Value;
                            baseRateInfo.BR_40HC = kvp.Value;
                            baseRateInfo.BR_45 = kvp.Value;
                        }
                    }
                    else if (kvp.Key == "20'")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            baseRateInfo.BR_20 = kvp.Value;
                    }
                    else if (kvp.Key == "40'")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            baseRateInfo.BR_40 = kvp.Value;
                    }
                    else if (kvp.Key == "40hc")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            baseRateInfo.BR_40HC = kvp.Value;
                    }
                    else if (kvp.Key == "45'")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            baseRateInfo.BR_45 = kvp.Value;
                    }
                    else if (kvp.Key == "cmdt")
                    {

                    }
                    else if (kvp.Key == "directcall")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            miscRateInfo.customNotes = kvp.Value;
                    }
                    else if (kvp.Key == "note")
                    {
                        if (!String.IsNullOrWhiteSpace(kvp.Value))
                            miscRateInfo.numberNotes = kvp.Value;
                    }
                }
                if (String.IsNullOrWhiteSpace(baseRateInfo.BR_20) && String.IsNullOrWhiteSpace(baseRateInfo.BR_40) && String.IsNullOrWhiteSpace(baseRateInfo.BR_40HC) && String.IsNullOrWhiteSpace(baseRateInfo.BR_45))
                {
                    //if (!String.IsNullOrWhiteSpace(inlandDetails.inland_main))
                    //    convertedData.Rows[convertedData.Rows.Count - 1]["INLAND"] += " " + inlandDetails.inland_main;

                    //if (!String.IsNullOrWhiteSpace(miscRateInfo.contractNotes))
                    //    convertedData.Rows[convertedData.Rows.Count - 1]["CONTRACT_NOTES"] += " " + miscRateInfo.contractNotes;

                    //if (!String.IsNullOrWhiteSpace(miscRateInfo.mode))
                    //    convertedData.Rows[convertedData.Rows.Count - 1]["MODE"] += miscRateInfo.mode;
                    //POSSIBLE OTHERS IN THE FUTURE TO BE ADDED HERE
                }
                else
                {

                    cityInfo.cityViaDetail = (header as HeaderArb).ApplicableOver;
                    cityInfo.cityViaDetail = (header as HeaderArb).ApplicableOver;
                    scope.trade = (header as HeaderArb).Scope;
                    baseRateInfo.rateType = (header as HeaderArb).ArbsView;
                    miscRateInfo.customNotes = special_notes;
                    inlandDetails.setOtherInfo(_ID, _GID, baseRateInfo, miscRateInfo, cityInfo, notesInfo, scope, bd.RowColorSchemes);

                    items.Add((inlandDetails as T));
                    _ID++;
                }
            }
            //FOOTER
            //var rateNotes = new NotesDetails(_TID, footer);
            //rateItems.Add(rateNotes);
            string pointerStr = "";
            if((header as HeaderArb).ArbsView == RateType.OARB)
            {
                pointerStr = "Origin";
            }else
            {
                pointerStr = "Destination";
            }

            var groupD = new GroupDetails(_GID, header.CurrentLine, (header as HeaderArb).ApplicableOver, footer.NoteString, (header as HeaderArb).Scope, pointerStr, new List<NotesDetails>(), footer.RowColorSchemes);
            groupItems.Add(groupD);
            _GID++;

            //_TID++;
        }
       
    }
}
