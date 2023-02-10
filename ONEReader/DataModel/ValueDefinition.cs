﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    public class ValueDefinition
    {
        [XmlElement("COLOR_TYPE")]
        public XLColorType colorType { get; set; }
        [XmlElement("COLOR_VALUE")]
        public Color colorValue { get; set; }
        [XmlElement("COLOR_INDEX")]
        public int indexColor { get; set; }
        [XmlElement("COLOR_THEME")]
        public XLThemeColor themeColor { get; set; }
        [XmlElement("TEST3")]

        public string textValue { get; set; }
       
        public ValueDefinition()
        {
            textValue = "";
        }
    }
}
