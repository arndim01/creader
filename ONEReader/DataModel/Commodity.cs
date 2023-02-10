using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class Commodity
    {
        [XmlElement("COMMODITY_CODE")]
        public string _commodityCode { get; set; }
        [XmlElement("COMMODITY_DESC")]
        public string _commodityDesc { get; set; }
        [XmlElement("COMMODITY_EXCL")]
        public string _commodityExcl { get; set; }
        [XmlElement("COMMODITY_NAC")]
        public string _commodityNac { get; set; }
        [XmlElement("COMMODITY_NOTES")]
        public string _commodityNotes { get; set; }
        public Commodity(string commodityCode, string commodityDesc, string commodityExcl, string commodityNac)
        {
            _commodityCode = commodityCode;
            _commodityDesc = commodityDesc;
            _commodityExcl = commodityExcl;
            _commodityNac = commodityNac;
            _commodityNotes = "";
        }
        public Commodity() { }

    }
}
