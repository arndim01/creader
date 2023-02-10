using CMAReader.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Build
{
    public class ContractSheets : IContractSheets, IEnumerable<IContractSheet>
    {
        private ContractProperties _ContractProperties;
        private Dictionary<string, ContractSheet> _ContractSheetDictionary = new Dictionary<string, ContractSheet>();
        public ContractSheets(ContractProperties ContractProperties)
        {
            _ContractProperties = ContractProperties;
        }
        public IContractSheet Add(string SheetName)
        {
            var Sheet = new ContractSheet(SheetName, _ContractProperties);
            _ContractSheetDictionary.Add(SheetName.ToLowerInvariant(), Sheet);
            return Sheet;
        }
        public IEnumerator<IContractSheet> GetEnumerator()
        {
            return ((IEnumerable<IContractSheet>)_ContractSheetDictionary.Values).GetEnumerator();
        }
        IEnumerator<IContractSheet> IEnumerable<IContractSheet>.GetEnumerator()
        {
            return _ContractSheetDictionary.Values.Cast<IContractSheet>().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
