using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    [XmlRoot("RATE_LINE")]
    public class RateDetails 
    {
        [XmlElement("ID")]
        public int _ID { get; set; }
        [XmlElement("GROUP_ID")]
        public int _GID { get; set; }
        [XmlElement("TABLE_ID")]
        public int _TID { get; set; }
        [XmlElement("VALIDITY_INFO")]
        public ContractValidity _contractValidity { get; set; }
        //Commodity Info+
        [XmlElement("COMMODITY")]
        public Commodity _commodityInfo { get; set; }
        //RATE INFO
        [XmlElement("ORIGIN_INFO")]
        public CityInfo _originInfo { get; set; }
        [XmlElement("DESTINATION_INFO")]
        public CityInfo _destinationInfo { get; set; }
        //BASE RATE INFO
        [XmlElement("BASERATE_INFO")]
        public BaseRateInfo _baseRateInfo { get; set; }
        //Other INFO
        [XmlElement("MISCRATE_INFO")]
        public MiscRateInfo _miscRateInfo { get; set; }
        [XmlElement("NOTES_INFO")]
        public Notes _notes { get; set; }
        [XmlElement("SCOPE_INFO")]
        public Scope _scope { get; set; }
        [XmlElement("COLOR_SCHEME")]
        public List<RowColorScheme> _rowColorSchemes { get; set; }
        //[XmlElement("COLOR_SCHEME_COUNT")]
        //public int _colorSchemeCount { get; set; }
        public RateDetails(int ID, int GID, int TID, ContractValidity contractValidity, Commodity commodityInfo, CityInfo originInfo,
            CityInfo destinationInfo, BaseRateInfo baseRateInfo, MiscRateInfo miscRateInfo, Notes notes, Scope scope, List<RowColorScheme> rowColorSchemes)
        {
            _ID = ID;
            _GID = GID;
            _TID = TID;
            _contractValidity = contractValidity;
            _commodityInfo = commodityInfo;
            _originInfo = originInfo;
            _destinationInfo = destinationInfo;
            _baseRateInfo = baseRateInfo;
            _miscRateInfo = miscRateInfo;
            _notes = notes;
            _scope = scope;
            _rowColorSchemes = rowColorSchemes;
            //_colorSchemeCount = rowColorSchemes.Count;
        }
        public RateDetails() { }
    }
}
