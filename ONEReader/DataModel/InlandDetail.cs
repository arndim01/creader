using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    [XmlRoot("INLAND")]
    public class InlandDetail 
    {
        [XmlElement("ID")]
        public int _ID { get; set; }
        [XmlElement("TABLE_ID")]
        public int _TID { get; set; }
        [XmlElement("VALIDITY_INFO")]
        public ContractValidity _contractValidity { get; set; }
        [XmlElement("INLAND_INFO")]
        public CityInfo inland_info { get; set; }
        [XmlElement("BASERATE_INFO")]
        public BaseRateInfo _baseRateInfo { get; set; }
        [XmlElement("MISCRATE_INFO")]
        public MiscRateInfo _miscRateInfo { get; set; }
        [XmlElement("NOTES_INFO")]
        public Notes _notes { get; set; }
        [XmlElement("SCOPE_INFO")]
        public Scope _scope { get; set; }
        [XmlElement("COLOR_SCHEME")]
        public List<RowColorScheme> _rowColorSchemes { get; set; }
        public void setOtherInfo(int ID, int TID, BaseRateInfo baseRateInfo, MiscRateInfo miscRateInfo, 
            CityInfo in_info, Notes notes, Scope scope, List<RowColorScheme> rowColorSchemes)
        {
            _ID = ID;
            _TID = TID;
            _baseRateInfo = baseRateInfo;
            _miscRateInfo = miscRateInfo;
            inland_info = in_info;
            _notes = notes;
            _scope = scope;
            _rowColorSchemes = rowColorSchemes;
        }
        public InlandDetail(ContractValidity contractValidity) {
            _contractValidity = contractValidity;
        }
        public InlandDetail()
        {

        }

    }
}
