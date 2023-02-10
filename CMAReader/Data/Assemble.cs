using CMAReader.Build;
using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Data
{
    public class Assemble : IDisposable
    {
        private readonly IPatternXML _PatternXML;
        private readonly IContractSheets _ContractSheets;
        public Assemble(IContractSheets ContractSheets)
        {
            _PatternXML = new PatternXML();
            _ContractSheets = ContractSheets;
        }
        public void ReadData()
        {
            foreach(IContractSheet ContractSheet in _ContractSheets)
            {
                var CmaSheet = new Sheet(ContractSheet);
                if (CmaSheet.IsValid)
                {
                    





                }
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
