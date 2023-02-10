using ONEReader.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class MiscRateInfo
    {
        [XmlElement("MODE")]
        public string mode { get; set; }
        [XmlElement("CUSTOM_NOTES")]
        public string customNotes { get; set; }

        [XmlElement("NUMBER_NOTES")]
        public string numberNotes { get; set; }
        [XmlElement("HAZ_TYPE")]
        public string hazType { get; set; }
        [XmlElement("ORG_SERVICE")]
        public string o_service { get; set; }

        [XmlElement("DEST_SERVICE")]
        public string d_service { get; set; }
        public MiscRateInfo() {
            hazType = "";
            mode = "";
            customNotes = "";
            numberNotes = "";
            o_service = "";
            d_service = "";
        }
    }
}
