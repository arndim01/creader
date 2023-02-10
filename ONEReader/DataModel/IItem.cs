using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.DataModel
{
    public interface IItem<T>: IEnumerable
    {
        void Add(T item);
    }
}
