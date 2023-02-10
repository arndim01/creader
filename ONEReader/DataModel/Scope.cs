using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class Scope
    {
        [XmlElement("TRADE")]
        public string trade { get; set; }
        [XmlElement("BULLET")]
        public string bullet { get; set; }
        public Scope()
        {
            trade = "";
            bullet = "";
        }
    }
}
