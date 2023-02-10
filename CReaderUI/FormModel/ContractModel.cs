using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CReaderUI.FormModel
{
    public class ContractModel
    {
        public string effectiveDate { get; set; }
        public string expirationDate { get; set; }
        public string amendmentId { get; set; }
        public string contractId { get; set; }
        public string contractPath { get; set; }
        public Carrier carrier { get; set; }
        public ContractModel() { }
    }
}
