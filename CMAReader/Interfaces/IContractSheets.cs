using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Interfaces
{
    public interface IContractSheets: IEnumerable<IContractSheet>
    {
        IContractSheet Add(string SheetName);
    }
}
