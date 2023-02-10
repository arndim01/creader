
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter.Build
{
    public interface ITemplateBuilder
    {
        void WRITE_TEMPLATE_COLUMN(params object[] obj);
        void WRITE_TEMPLATE_DATA(params object[] obj);
        DataTable details { get; set; }
        DataTable details2 { get; set; }
    }
}
