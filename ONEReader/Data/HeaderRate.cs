using ONEReader.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONEReader.DataModel;
using ONEReader.Enum;

namespace ONEReader.Data
{
    public class HeaderRate : IHeader
    {
        public string Bullet { get; set; }
        public string Commodity { get; private set; }
        public string Origin { get; private set; }
        public string OriginVia { get; private set; }
        public string Scope { get; private set; }


        private bool ScopeStillExist { get; set; }
        private bool CommodityStillExist { get; set; }
        private bool OriginStillExist { get; set; }
        private bool OriginViaStillExist { get; set; }


        private bool GetFirstScopeValue { get; set; }
        private bool GetFirstCommodityValue { get; set; }
        private bool GetFirstOriginValue { get; set; }
        private bool GetFirstOriginViaValue { get; set; }


        public RateType Type
        {
            get
            {
                return RateType.RATE;
            }
        }

        public ForeCastHeader ForeHeader { get; set; }

        public bool CommodityOnchanged { get; set; }

        public int CurrentLine { get; set; }

        public HeaderRate()
        {
            StartNewExtract();
        }

        public void StartNewExtract()
        {
            ScopeStillExist = false;
            CommodityStillExist = false;
            OriginStillExist = false;
            OriginViaStillExist = false;

            GetFirstScopeValue = false;
            GetFirstCommodityValue = false;
            GetFirstOriginValue = false;
            GetFirstOriginViaValue = false;
        }

        public void ExtractHeader(string headerValue, int currentline)
        {
            if (IsScope(headerValue) || IsCommodity(headerValue) || IsOrigin(headerValue) || IsOriginVia(headerValue))
            {
                ScopeStillExist = IsScope(headerValue);
                CommodityStillExist = IsCommodity(headerValue);
                OriginStillExist = IsOrigin(headerValue);
                OriginViaStillExist = IsOriginVia(headerValue);
            }

            if (ScopeStillExist)
            {
                if (IsScope(headerValue) && !GetFirstScopeValue)
                {
                    Scope = headerValue.getScopeValue();
                    InitializeHeader(1);
                }
                else
                    Scope += headerValue;
            }

            if (CommodityStillExist)
            {
                if (IsCommodity(headerValue) && !GetFirstCommodityValue)
                {
                    Bullet = headerValue.getBulletValue();
                    Commodity = headerValue.getCommodityValue();
                    InitializeHeader(2);
                    CurrentLine = currentline;
                }
                else
                {
                    if(!OriginStillExist && !OriginViaStillExist)
                    {
                        Commodity += " " + headerValue;
                    }
                }

                CommodityOnchanged = true;
            }
            
            if (OriginStillExist)
            {
                if (IsOrigin(headerValue) && !GetFirstOriginValue)
                {
                    Origin = headerValue.getOrigin();
                    OriginVia = ""; // if origin exist always empty the origin via
                    InitializeHeader(3);
                }
                else
                {
                    if( !OriginViaStillExist)
                    {
                        Origin += " " + headerValue.Trim();
                    }
                }
            }

            if (OriginViaStillExist)
            {
                if (IsOriginVia(headerValue) && !GetFirstOriginViaValue)
                {
                    OriginVia = headerValue.getOriginVia();
                    InitializeHeader(4);
                }
                else
                    OriginVia += " " + headerValue.Trim();
            }
            
        }

        private void InitializeHeader(int id)
        {
            GetFirstScopeValue = (id == 1 ? true : false);
            GetFirstCommodityValue = (id == 2 ? true : false);
            GetFirstOriginValue = (id == 3 ? true : false);
            GetFirstOriginViaValue = (id == 4 ? true : false);
        }

        private bool IsCommodity(string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("commodity:");
        }

        private bool IsOrigin(string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("origin:");
        }

        private bool IsOriginVia(string headderValue)
        {
            return headderValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("originvia:");
        }

        private bool IsScope(string headerValue)
        {
            return headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("[") && headerValue.UnescapeValue().Replace(" ", "").ToLowerInvariant().Contains("]");
        }
    }
}
