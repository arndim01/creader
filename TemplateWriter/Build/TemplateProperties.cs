
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter.Build
{
    public class TemplateProperties
    {
        public TemplateProperties(string path)
        {
            contractPath = path;
        }
        public string contractPath { get; set; }
      
    

    }
}
