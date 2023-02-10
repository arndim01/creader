using ClosedXML.Excel;
using ONEReader.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    public class TableContent
    {
        public Dictionary<string, string> CellValue { get; set; }
        public List<RowColorScheme> RowColorSchemes { get; set; } 
        public TableContent()
        {
            CellValue = new Dictionary<string, string>();
            RowColorSchemes = new List<RowColorScheme>();
        }
    }
    public class FooterContent
    {
        public string NoteString { get; set; }
        public List<RowColorScheme> RowColorSchemes { get; set; }
        public FooterContent()
        {
            NoteString = "";
            RowColorSchemes = new List<RowColorScheme>();
        }
    }
    public class RowColorScheme
    {
        [XmlElement("COLOR_TYPE")]
        public string ColorType { get; set; }
        [XmlElement("COLOR_VALUE")]
        public string ColorValue { get; set; }
        [XmlElement("COLOR_INDEX")]
        public string IndexColor { get; set; }
        [XmlElement("COLOR_THEME")]
        public string ThemeColor { get; set; }
        [XmlElement("TEXT_VALUE")]
        public string TextValue { get; set; }
        [XmlElement("ID_VALUE")]
        public string IdValue { get; set; }
        [XmlElement("DEFAULT_SCHEME")]
        public ColorValueDefault ColorValueDefault { get; set; }
        public RowColorScheme()
        {
            TextValue = "";

        }
    }
}