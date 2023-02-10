using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class ContractValidity
    {
        [XmlElement("EFFECTIVE_DATE")]
        public string _eff { get; set; }
        [XmlElement("EXPIRATION_DATE")]
        public string _exp { get; set; }
        public ContractValidity(string eff, string exp)
        {
            _eff = eff;
            _exp = exp;
        }
        public ContractValidity()
        {

        }
    }
}
