using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class OriginInfo 
    {
        [XmlElement("ORIGIN_DETAIL")]
        public string origin_detail { get; set; }
        [XmlElement("ORIGIN_DETAIL_VIA")]
        public string origin_detail_via { get; set; }
        [XmlElement("ORIGIN_MAIN")]
        public string origin_main { get; set; }
        [XmlElement("ORIGIN_MAIN_VIA")]
        public string origin_main_via { get; set; }
        public OriginInfo(string org, string org_det, string org_via, string org_via_det)
        {
            origin_main = org;
            origin_detail = org_det;
            origin_main_via = org_via;
            origin_detail_via = org_via_det;
        }
        public OriginInfo() { }
    }
}
