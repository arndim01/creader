using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Build
{
    public interface IPatternXML
    {
        string[] MAINSEARCH { get; }
        string[] MAINSEARCH_RATES { get; }
        string[] MAINSEARCH_ARBS { get; }
        string ORIGIN_ARBS { get;}
        string DESTINATION_ARBS { get; }
        string[] KEYWORDS { get; }
        string TABLESEARCH_RATES { get;  }
        string TABLESEARCH_VIASEARCH { get;  }
        string TABLESEARCH_ARBS { get;  }
        string DUPLICATE_RATE_COLUMN { get;  }
        string[] KEYWORDS_RATE_COLUMN { get; }
        string[] KEYWORDS_BASERATE_COLUMN { get; }
        string[] KEYWORDS_ARBS_COLUMN { get; }
        string[] KEYWORDS_BASEARBSRATE_COLUMN { get;  }
        string[] KEYWORDS_RATE_GENERALNOTE { get; }

        //KILL DATA LOOK UP
        string[] KILL_SEARCH { get;}

    }
}
