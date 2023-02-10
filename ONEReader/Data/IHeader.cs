using ONEReader.DataModel;
using ONEReader.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public interface IHeader
    {
        RateType Type { get; }
        ForeCastHeader ForeHeader { get; set; }
        void ExtractHeader(string headerValue, int currentLine);
        bool CommodityOnchanged { get; set; }
        int CurrentLine { get; set; }
        void StartNewExtract();
    }
}
