using CMAReader.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CMAReader.Build
{
    public class PatternXML: IPatternXML
    {
        private readonly Dictionary<string, string[]> _Pattern;
        public PatternXML()
        {
            _Pattern = new Dictionary<string, string[]>();
            List<string> items = new List<string>();
            string stringXML = Properties.Resources.CMA_PATTERN_V1;
            using(XmlReader reader = XmlReader.Create(new StringReader(stringXML)))
            {
                string curKey = "";
                while(reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if(reader.Name == "patternkey")
                            {
                                curKey = reader["name"];
                            }
                            break;
                        case XmlNodeType.Text:
                            items.Add(reader.Value.Trim());
                            break;
                        case XmlNodeType.EndElement:
                            if( reader.Name == "patternkey")
                            {
                                _Pattern.Add(curKey, items.ToArray());
                                items.Clear();
                            }
                            break;
                    }
                }
            }
        }
        public IEnumerable GetValidSheetKeywords => _Pattern["valid_sheet_keywords"].ToList();

        public string GetValidCommodityHeaderKeyword => String.Join("", _Pattern["cma_commodity_header"].ToList());
        public string GetValidCommodityTableKeyword => String.Join("", _Pattern["cma_commodity_table"].ToList());
        public IEnumerable GetValidCommodityColumnKeywords => _Pattern["cma_commodity_table_column"].ToList();

        public string GetValidPortGroupHeaderKeyword => String.Join("", _Pattern["cma_portcode_header"].ToList());
        public string GetValidPortGroupTableKeyword => String.Join("", _Pattern["cma_portcode_table"].ToList());
        public IEnumerable GetValidPortGroupColumnKeywords => _Pattern["cma_portcode_table_column"].ToList();

        public string GetValidDryRateHeaderKeyword => String.Join("", _Pattern["cma_rate_header"].ToList());
        public string GetValidDryRateTableKeyword => String.Join("", _Pattern["cma_rate_table"].ToList());
        public IEnumerable GetValidDryRateColumnKeywords => _Pattern["cma_rate_table_column"].ToList();

        public string GetValidReeferRateHeaderKeyword => String.Join("", _Pattern["cma_rate-re_header"].ToList());
        public string GetValidReeferRateTableKeyword => String.Join("", _Pattern["cma_rate-re_table"].ToList());
        public IEnumerable GetValidReeferRateColumnKeywords => _Pattern["cma_rate-re_table_column"].ToList();

        public string GetValidSpecialRateHeaderKeyword => String.Join("", _Pattern["cma_rate-sp_header"].ToList());
        public string GetValidSpecialRateTableKeyword => String.Join("", _Pattern["cma_rate-sp_table"].ToList());
        public IEnumerable GetValidSpecialRateColumnKeywords => _Pattern["cma_rate-sp_table_column"].ToList();

        public string GetValidOriginArbHeaderKeyword => String.Join("", _Pattern["cma_o_arb_header"].ToList());
        public string GetValidOriginArbTableKeyword => String.Join("", _Pattern["cma_o_arb_table"].ToList());
        public IEnumerable GetValidOriginArbColumnKeywords => _Pattern["cma_o_arb_table_column"].ToList();

        public string GetValidDestinationArbHeaderKeyword => String.Join("", _Pattern["cma_d_arb_header"].ToList());
        public string GetValidDestinationArbTableKeyword => String.Join("", _Pattern["cma_d_arb_table"].ToList());
        public IEnumerable GetValidDestinationArbColumnKeywords => _Pattern["cma_d_arb_table_column"].ToList();

        public string GetValidGeneralSurchargeHeaderKeyword => String.Join("", _Pattern["cma_general_surcharge_header"].ToList());
        public string GetValidGeneralSurchargeTableKeyword => String.Join("", _Pattern["cma_general_surcharge_table"].ToList());
        public string GetValidGeneralSurchargeSpecialWord => String.Join("", _Pattern["cma_general_surcharge_special_column"].ToList());
        public IEnumerable GetValidGeneralSurchargeColumnKeywords => _Pattern["cma_general_surcharge_table_column"].ToList();

        public string GetValidGRIHeaderKeyword => String.Join("", _Pattern["cma_gri_header"].ToList());
        public string GetValidGRITableKeyword => String.Join("", _Pattern["cma_gri_table"].ToList());
        public IEnumerable GetValidGRIColumnKeywords => _Pattern["cma_gri_table_column"].ToList();

        public string GetValidOSPFHeader1Keyword => String.Join("", _Pattern["cma_ospf_header1"].ToList());
        public string GetValidOSPFHeader2Keyword => String.Join("", _Pattern["cma_ospf_header2"].ToList());
        public string GetValidOSPFTableKeyword => String.Join("", _Pattern["cma_ospf_table"].ToList());
        public IEnumerable GetValidOSPFColumnKeywords =>  _Pattern["cma_ospf_table_column"].ToList();

        public string GetValidNote4HeaderKeyword => String.Join("", _Pattern["cma_note4_header"].ToList());
        public string GetValidNote4TableKeyword => String.Join("", _Pattern["cma_note4_table"].ToList());
        public IEnumerable GetValidNote4ColumnKeywords => _Pattern["cma_note4_table_column"].ToList();

        public string GetValidFreetimeHeaderKeyword => String.Join("", _Pattern["cma_freetime_header"].ToList());
        public string GetValidFreetimeTableKeyword => String.Join("", _Pattern["cma_freetime_table"].ToList());
        public IEnumerable GetValidFreetimeColumnKeywords => _Pattern["cma_freetime_table_column"].ToList();
    }
}
