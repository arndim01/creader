
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class DLJsonModel
    {

        public JToken _c_detail { get; set; }
        public JArray _commodity { get; set; }
        public JArray _rates { get; set; }
        public JArray _rates_header { get; set; }
        public JArray _rates_surcharge_header { get; set; }
        public JArray _city { get; set; }
        public JArray _arbs { get; set; }

        public DLJsonModel(JToken c_detail, JArray commodity, JArray rates, 
            JArray rates_header, JArray rates_surcharge_header, JArray city, JArray arbs)
        {

            _c_detail = c_detail;
            _commodity = commodity;
            _rates = rates;
            _rates_header = rates_header;
            _rates_surcharge_header = rates_surcharge_header;
            _city = city;
            _arbs = arbs;
        }
    }
}
