using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    public class ContractInfo
    {

        internal string contractId;
        internal string carrier;
        internal string amdId;
        internal DateTime eff;
        internal DateTime exp;
        public ContractInfo(string _carrier, string _contId, string _amd, string _eff, string _exp)
        {
            carrier = _carrier;
            contractId = _contId;
            amdId = _amd;
            eff = Convert.ToDateTime(_eff);
            exp = Convert.ToDateTime(_exp);

        }
        public ContractInfo() { }
    }
}
