using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class Notes
    {
        [XmlElement("RATELINE_NOTES")]
        public string rateLineNotes { get; set; }
        public Notes()
        {
            rateLineNotes = "";
        }
    }
}
