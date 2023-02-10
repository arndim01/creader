using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public enum Carrier: int
    {
        [StringValue("ONE LINE")]
        ONE = 0,
        [StringValue("CMA")]
        CMA = 1
    }
}
