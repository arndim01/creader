using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ONEReader.Build
{
    internal class PatternXML : IPatternXML
    {
        private Dictionary<string, string[]> _pattern;
        internal PatternXML()
        {
            //string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            //string Filename = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/nyk_pattern_version_1.xml";
            _pattern = new Dictionary<string, string[]>();
            List<string> items = new List<string>();
            string stringXML = Properties.Resources.nyk_pattern_version_1;
            using (XmlReader reader = XmlReader.Create(new StringReader(stringXML)))
            {
                string curKey = "";
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "patternkey")
                            {
                                curKey = reader["name"];
                            }
                            break;
                        case XmlNodeType.Text:
                            items.Add(reader.Value.Trim());
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "patternkey")
                            {
                                _pattern.Add(curKey, items.ToArray());
                                items.Clear();
                            }
                            break;
                    }
                }
            }
        }
        public string DESTINATION_ARBS
        {
            get
            {
                return String.Join("", _pattern["destination_arbs"]);
            }
        }

        public string DUPLICATE_RATE_COLUMN
        {
            get
            {
                return String.Join("", _pattern["key_duplicate"]);
            }
        }

        public string[] KEYWORDS
        {
            get
            {
                return _pattern["keywords"];
            }
        }

        public string[] KEYWORDS_ARBS_COLUMN
        {
            get
            {
                return _pattern["key_words_body_arbs"];
            }
        }

        public string[] KEYWORDS_BASEARBSRATE_COLUMN
        {
            get
            {
                return _pattern["key_words_rates_arbs"];
            }
        }

        public string[] KEYWORDS_BASERATE_COLUMN
        {
            get
            {
                return _pattern["key_words_rates"];
            }
        }

        public string[] KEYWORDS_RATE_COLUMN
        {
            get
            {
                return _pattern["key_words_body"];
            }
        }

        public string[] KEYWORDS_RATE_GENERALNOTE
        {
            get
            {
                return _pattern["key_words_notes"];
            }
        }

        public string[] KILL_SEARCH
        {
            get
            {
                return _pattern["kill_search"];
            }
            
        }

        public string[] MAINSEARCH
        {
            get
            {
                return _pattern["main_search"];
            }
        }

        public string[] MAINSEARCH_ARBS
        {
            get
            {
                return _pattern["main_search_arbs"];
            }
        }

        public string[] MAINSEARCH_RATES
        {
            get
            {
                return _pattern["main_search_rates"];
            }
        }
        

        public string ORIGIN_ARBS
        {
            get
            {
                return String.Join("",_pattern["origin_arbs"]);
            }
        }

        public string TABLESEARCH_ARBS
        {
            get
            {
                return String.Join("", _pattern["table_arbs_search"]);
            }
        }

        public string TABLESEARCH_RATES
        {
            get
            {
                return String.Join("", _pattern["table_rates_search"]);
            }
        }

        public string TABLESEARCH_VIASEARCH
        {
            get
            {
                return String.Join("", _pattern["table_arbsvia_search"]);
            }
        }
    }
}
