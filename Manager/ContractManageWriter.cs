using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TemplateWriter.Data;

namespace Manager
{
    public class ContractManageWriter
    {

        private DLJsonModel _dl { get; set; }
        public ContractManageWriter(DLJsonModel dl)
        {
            _dl = dl;
        }
        public void createContract()
        {
            Assemble ass = new Assemble();
            ass.CONSTRUCT_TEMPLATE(_dl._c_detail, _dl._commodity, _dl._rates, _dl._rates_header, _dl._rates_surcharge_header, _dl._city, _dl._arbs);

        }
        
    }
}
