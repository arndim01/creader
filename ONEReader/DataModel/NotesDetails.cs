using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class NotesDetails
    {
        [XmlElement("ID")]
        public int _ID { get; set; }
        [XmlElement("HEADER")]
        public string _HEADER { get; set; }
        [XmlElement("NOTES")]
        public string _NOTES { get; set; }
        [XmlElement("COLOR_SCHEME")]
        public List<RowColorScheme> _rowColorSchemes { get; set; }
        [XmlElement("COLOR_SCHEME_COUNT")]
        public int _colorSchemeCount { get; set; }
        public NotesDetails() { }
        public NotesDetails(int id, string header, string notes, List<RowColorScheme> rowColorSchemes)
        {
            _ID = id;
            _HEADER = header;
            _NOTES = notes;

            if( !String.IsNullOrWhiteSpace(notes))
            {
                _rowColorSchemes = rowColorSchemes;
                _colorSchemeCount = rowColorSchemes.Count;
            }

        }
    }
}
