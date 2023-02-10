using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class GroupDetails
    {
        [XmlElement("ID")]
        public int _ID { get; set; }
        [XmlElement("HEADER")]
        public string _HEADER { get; set; }
        [XmlElement("LOCATION")]
        public int _LOCATION { get; set; }
        [XmlElement("NOTES")]
        public string _NOTES { get; set; }
        [XmlElement("SCOPE")]
        public string _SCOPE { get; set; }
        [XmlElement("RATE_ITEMS")]
        public List<NotesDetails> _RATE_NOTES { get; set; }
        [XmlElement("TOTAL_NOTES")]
        public int _TOTAL_RNOTES { get; set; }
        [XmlElement("EXTRACT_FROM")]
        public string _EXTRACTED { get; set; }
        [XmlElement("COLOR_SCHEME")]
        public List<RowColorScheme> _rowColorSchemes { get; set; }
        [XmlElement("COLOR_SCHEME_COUNT")]
        public int _colorSchemeCount { get; set; }
        public GroupDetails() { }
        public GroupDetails(int id, int location, string header, string notes, string scope, 
            string extracted,  List<NotesDetails> rateNotes, List<RowColorScheme> rowColorSchemes)
        {
            _ID = id;
            _LOCATION = location;
            _HEADER = String.IsNullOrWhiteSpace(header)  ? "" : header;
            _NOTES =  String.IsNullOrWhiteSpace(notes) ? "" : notes;
            _SCOPE = String.IsNullOrWhiteSpace(scope) ? "" : scope;
            _RATE_NOTES = new List<NotesDetails>();
            _RATE_NOTES.AddRange(rateNotes);
            _TOTAL_RNOTES = rateNotes.Count;
            _EXTRACTED = extracted;



            _rowColorSchemes = rowColorSchemes;



            _colorSchemeCount = rowColorSchemes.Count();
        }
    }
}
