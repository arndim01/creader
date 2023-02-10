using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Build
{
    public class ContractInfo
    {
        public string _ContractId { get; set; }
        public string _Carrier { get; set; }
        public string _AmdId { get; set; }
        public DateTime _Eff { get; set; }
        public DateTime _Exp { get; set; }
        public ContractInfo(string ContractId, string Carrier, string AmdId, string Eff, string Exp)
        {
            _Carrier = Carrier;
            _ContractId = ContractId;
            _AmdId = AmdId;
            _Eff = Convert.ToDateTime(Eff);
            _Exp = Convert.ToDateTime(Exp);
            
        }
        public ContractInfo() { }
    }
}
