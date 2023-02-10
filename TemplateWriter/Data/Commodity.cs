using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter.Build;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Data;

namespace TemplateWriter.Data
{
    public class Commodity : ITemplateBuilder
    {
        private TemplateProperties prop;
        public DataTable details { get; set; }

        public DataTable details2 { get; set; }

        public Commodity(TemplateProperties properties)
        {
            prop = properties;
            details = new DataTable("commodity");
           
        }
        public void WRITE_TEMPLATE_COLUMN(params object[] obj)
        {
            List<string> main_column = TemplateWriter.constructListValueOfEnum(new TemplateConstructModel.CommodityColumns { });
            foreach (string m1 in main_column)
            {
                details.Columns.Add(m1);
            }
        }

        public void WRITE_TEMPLATE_DATA(params object[] obj )
        {
            JArray commodityData = (JArray) obj[0];
            JToken contractDetail = (JToken) obj[1];
            foreach (JToken comm in commodityData)
            {
                details.Rows.Add(
                    contractDetail[0].Value<string>(),
                    contractDetail[3].Value<string>(),
                    contractDetail[4].Value<string>(),
                    "",
                    contractDetail[1].Value<string>(),
                    contractDetail[2].Value<string>(),
                    //
                    comm[1].Value<string>(),
                    comm[2].Value<string>(),
                    comm[2].Value<string>(),
                    comm[3].Value<string>(),
                    comm[4].Value<string>(),
                    "");
            }
            //return commodityResult;
        }

        //COMMODITY HELPER
   

    }
}
