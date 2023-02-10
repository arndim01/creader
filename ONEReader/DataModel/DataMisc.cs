using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class DataMisc
    {
        [XmlElement("TOTAL_RATES")]
        public int TotalRates { get; set; }
        [XmlElement("TOTAL_ARBS")]
        public int TotalArbs { get; set; }
        [XmlElement("TOTAL_GROUPNOTES_RATES")]
        public int TotalGroupNotes_RATES { get; set; }
        [XmlElement("TOTAL_GROUPNOTES_ARBS")]
        public int TotalGroupNotes_ARBS { get; set; }

        public DataMisc(int trates, int tarbs, int tgn_rates, int tgn_arbs)
        {
            TotalRates = trates;
            TotalArbs = tarbs;
            TotalGroupNotes_RATES = tgn_rates;
            TotalGroupNotes_ARBS = tgn_arbs;
        }
        public DataMisc() { }

    }
}
