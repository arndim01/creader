using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    public interface IContractsheets: IEnumerable<IContractsheet>
    {
        IContractsheet Add(string sheetName);
    }
}
