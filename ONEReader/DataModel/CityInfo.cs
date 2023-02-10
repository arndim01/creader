using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class CityInfo
    {
        [XmlElement("CITY_DETAIL")]
        public string cityDetail { get; set; }
        [XmlElement("CITY_VIA_DETAIL")]
        public string cityViaDetail { get; set; }
        [XmlElement("CITY_POINTER")]
        public In_Point inland_pointer { get; set; }
        public CityInfo(string in_detail, string in_v_detail, In_Point in_point)
        {
            cityDetail = in_detail;
            cityViaDetail = in_v_detail;
            inland_pointer = in_point;
        }
        public CityInfo()
        {
            cityDetail = "";
            cityViaDetail = "";
            inland_pointer = In_Point.NULL;
        }
    }
    public enum In_Point
    {
        NULL = 0,
        R_ORG = 1,
        R_DES = 2,
        A_ORG = 3,
        A_DES = 4
    }
}
