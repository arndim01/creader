using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Interfaces
{
    public interface IPatternXML
    {
        IEnumerable GetValidSheetKeywords { get; }

        string GetValidCommodityHeaderKeyword { get; }
        string GetValidCommodityTableKeyword { get; }
        IEnumerable GetValidCommodityColumnKeywords { get; }

        string GetValidPortGroupHeaderKeyword { get; }
        string GetValidPortGroupTableKeyword { get; }
        IEnumerable GetValidPortGroupColumnKeywords { get; }

        string GetValidDryRateHeaderKeyword { get; }
        string GetValidDryRateTableKeyword { get; }
        IEnumerable GetValidDryRateColumnKeywords { get; }

        string GetValidReeferRateHeaderKeyword { get; }
        string GetValidReeferRateTableKeyword { get; }
        IEnumerable GetValidReeferRateColumnKeywords { get; }

        string GetValidSpecialRateHeaderKeyword { get; }
        string GetValidSpecialRateTableKeyword { get; }
        IEnumerable GetValidSpecialRateColumnKeywords { get; }

        string GetValidOriginArbHeaderKeyword { get; }
        string GetValidOriginArbTableKeyword { get; }
        IEnumerable GetValidOriginArbColumnKeywords { get; }

        string GetValidDestinationArbHeaderKeyword { get; }
        string GetValidDestinationArbTableKeyword { get; }
        IEnumerable GetValidDestinationArbColumnKeywords { get; }

        string GetValidGeneralSurchargeHeaderKeyword { get; }
        string GetValidGeneralSurchargeTableKeyword { get; }
        string GetValidGeneralSurchargeSpecialWord { get; }
        IEnumerable GetValidGeneralSurchargeColumnKeywords { get; }

        string GetValidGRIHeaderKeyword { get; }
        string GetValidGRITableKeyword { get; }
        IEnumerable GetValidGRIColumnKeywords { get; }

        string GetValidOSPFHeader1Keyword { get; }
        string GetValidOSPFHeader2Keyword { get; }
        string GetValidOSPFTableKeyword { get; }
        IEnumerable GetValidOSPFColumnKeywords { get; }

        string GetValidNote4HeaderKeyword { get; }
        string GetValidNote4TableKeyword { get; }
        IEnumerable GetValidNote4ColumnKeywords { get; }

        string GetValidFreetimeHeaderKeyword { get; }
        string GetValidFreetimeTableKeyword { get; }
        IEnumerable GetValidFreetimeColumnKeywords { get; }
    }
}
