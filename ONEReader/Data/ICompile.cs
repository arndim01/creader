using ONEReader.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Data
{
    public interface ICompile<T>
    {
        DataTable convertedData { get; set; }
        IItem<T> items { get; set; }
        IItem<GroupDetails> groupItems { get; set; }
        //List<NotesDetails> rateItems { get; set; }
        void CompileData(IHeader header, List<TableContent> body, FooterContent footer);
    }
}
