using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONEReader.DataModel
{
    [Serializable]
    [XmlRoot("RATE_LINE")]
    public class ItemDetails<T> : IEnumerable, IItem<T>
    {
        [XmlElement("RATE")]
        List<T> _rateList = new List<T>();
        public void Add(T item)
        {
            _rateList.Add(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_rateList.ToList()).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
