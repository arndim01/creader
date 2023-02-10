using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class BasicInfo
    {
        [XmlElement("CARRIER")]
        public string _carrier { get; set; }
        [XmlElement("EFF")]
        public string _eff { get; set; }
        [XmlElement("EXP")]
        public string _exp { get; set; }
        [XmlElement("CONTRACT_ID")]
        public string _contractId { get; set; }
        [XmlElement("AMD_ID")]
        public string _amdId { get; set; }
        [XmlElement("EXTRACTED_FROM")]
        public string _extractedFrom { get; set; }
        public BasicInfo(string carrier, string eff, string exp, string contract, string amdId, string extractedFrom)
        {
            _carrier = carrier;
            _eff = eff;
            _exp = exp;
            _contractId = contract;
            _amdId = amdId;
        }
        public BasicInfo() { }
    }
}
