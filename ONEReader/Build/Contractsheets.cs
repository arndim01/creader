using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ONEReader.Build
{
    internal class Contractsheets : IContractsheets, IEnumerable<IContractsheet>
    {
        private ContractProperties _contractProperties;
        private Dictionary<string, Contractsheet> _contractSheet = new Dictionary<string, Contractsheet>();
        internal Contractsheets(ContractProperties contractProperties)
        {
            _contractProperties = contractProperties;
        }
        public IContractsheet Add(string sheetName)
        {
            var sheet = new Contractsheet(sheetName, _contractProperties);
            _contractSheet.Add(sheetName.ToLowerInvariant(), sheet);
            return sheet;
        }
        public IEnumerator<IContractsheet> GetEnumerator()
        {
            return ((IEnumerable<IContractsheet>)_contractSheet.Values).GetEnumerator();
        }
        IEnumerator<IContractsheet> IEnumerable<IContractsheet>.GetEnumerator()
        {
            return _contractSheet.Values.Cast<IContractsheet>().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
