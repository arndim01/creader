using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class DestinationInfo
    {
        [XmlElement("DESTINATION_DETAIL")]
        public string destination_detail { get; set; }
        [XmlElement("DESTINATION_DETAIL_VIA")]
        public string destination_detail_via { get; set; }
        [XmlElement("DESTINATION_MAIN")]
        public string destination_main { get; set; }
        [XmlElement("DESTINATION_MAIN_VIA")]
        public string destination_main_via { get; set; }
        public DestinationInfo(string dest, string dest_det, string dest_via, string dest_via_det)
        {
            destination_main = "";
            destination_detail = "";
            destination_main_via = "";
            destination_detail_via = "";
        }
        public DestinationInfo() {
            destination_detail = "";
            destination_detail_via = "";
            destination_main = "";
            destination_main_via = "";

        }
    }
}
