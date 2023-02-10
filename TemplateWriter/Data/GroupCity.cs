using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter.Build;

namespace TemplateWriter.Data
{
    public class GroupCity : ITemplateBuilder
    {
        private TemplateProperties prop;
        public DataTable details { get; set; }

        public DataTable details2 { get; set; }
        public GroupCity(TemplateProperties properties)
        {
            prop = properties;
            details = new DataTable("groupcode");
        }
        public void WRITE_TEMPLATE_COLUMN(params object[] obj)
        {
            List<string> main_column = TemplateWriter.constructListValueOfEnum(new TemplateConstructModel.GroupCodeColumn { });
            foreach(string m1 in main_column)
            {
                details.Columns.Add(m1);
            }
        }
        public void WRITE_TEMPLATE_DATA(params object[] obj)
        {
            JArray city = (JArray)obj[0];

            foreach( JToken tk in city)
            {
                if( tk[2] != null)
                {
                    if( tk[2].Count() > 0)
                    {
                        foreach(JToken min in tk[2])
                        {
                            details.Rows.Add(
                                tk[1].Value<string>(),
                                min[1].Value<string>(),
                                min[2].Value<string>(),
                                min[3].Value<string>(),
                                min[4].Value<string>()
                                );
                        }
                    }else
                    {
                        details.Rows.Add(
                            tk[1].Value<string>(),
                            "",
                            "",
                            "",
                            ""
                            );
                    }
                }else
                {
                    details.Rows.Add(
                            tk[1].Value<string>(),
                            "",
                            "",
                            "",
                            ""
                            );
                }
            }
        }
    }
}
