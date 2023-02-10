using ONEReader.DataHelper;
using ONEReader.DataModel;
using ONEReader.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public class HeaderArb : IHeader
    {
        public string ApplicableOver { get; private set; }
        public string Scope { get; private set; }
        public RateType ArbsView { get; private set; }

        public RateType Type
        {
            get
            {
                return ArbsView;
            }
        }

        public bool CommodityOnchanged
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentLine { get; set; }
        public ForeCastHeader ForeHeader { get; set; }

        public void StartNewExtract()
        {
        }
        public void ExtractHeader(string headerValue, int currentline)
        {
            if( isScope(headerValue))
            {
                Scope = headerValue.getScopeValue();
            }
            else if( isApplicableOver(headerValue))
            {
                ApplicableOver = headerValue.getApplicableOverValue();
                CurrentLine = currentline;
            }else if(isOArbsView(headerValue))
            {
                ArbsView = RateType.OARB;
            }
            else if (isDArbsView(headerValue))
            {
                ArbsView = RateType.DARB;
            }
        }
        private bool isOArbsView (string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("originarbitrary");
        }
        private bool isDArbsView (string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ","").ToLowerInvariant().Contains("destinationarbitrary");
        }
        private bool isApplicableOver(string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("applicableover:");
        }
        private bool isScope(string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("[") && headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("]");
        }

      
    }
}
