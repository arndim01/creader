using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{

    public enum RateType
    {
        NULL = 0,
        RATE = 1,
        OARB = 2,
        DARB = 3
    }
    [Serializable]
    public class BaseRateInfo
    {
        [XmlElement("BR_20")]
        public string BR_20 { get; set; }
        [XmlElement("BR_40")]
        public string BR_40 { get; set; }
        [XmlElement("BR_40HC")]
        public string BR_40HC { get; set; }
        [XmlElement("BR_45")]
        public string BR_45 { get; set; }
        [XmlElement("CONTAINER_TYPE")]
        public string containerType { get; set; }
        [XmlElement("CURRENCY")]
        public string currency { get; set; }
        [XmlElement("RATE_TYPE")]
        public RateType rateType { get; set; }
        public BaseRateInfo(string b20, string b40, string b40hc, string b45, string curr, string container, RateType rate)
        {
            BR_20 = "";
            BR_40 = "";
            BR_40HC = "";
            BR_45 = "";
            containerType = container;
            currency = curr;
            rateType = rate;
        }
        public BaseRateInfo() {

            BR_20 = "";
            BR_40 = "";
            BR_40HC = "";
            BR_45 = "";
            containerType = "";
            currency = "";
            rateType = RateType.NULL;

        }
    }
}
