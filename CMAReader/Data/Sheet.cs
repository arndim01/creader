using CMAReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAReader.Data
{
    public class Sheet : SheetAttributes
    {
        private readonly IContractSheet _ContractSheet;
        public Sheet(IContractSheet ContractSheet)
            : base(ContractSheet)
        {
            _ContractSheet = ContractSheet;
            ConstructAttributes();
        }

        private void ConstructAttributes()
        {
            //for (; _ContractSheet._CurrentLine < _ContractSheet._GetTotalRowsUsed; _ContractSheet.NextLine())
            //{
            //VALIDATE_COMMODITY_HEADER();
            //VALIDATE_PORTCODE_HEADER();
            try
            {


                VALIDATE_DRYRATES_HEADER();
                VALIDATE_REEFERRATES_HEADER();
                VALIDATE_SPECIALRATES_HEADER();
                VALIDATE_OARBS_HEADER();
                VALIDATE_DARBS_HEADER();
                VALIDATE_GENERAL_SURCHARGE_HEADER();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
            //VALIDATE_GRI_HEADER();
            //VALIDATE_OSPF_HEADER();
            //VALIDATE_NOTES_HEADER();
            //VALIDATE_FREETIME_HEADER();
            //}
            //_ContractSheet.ResetLine();

            Console.WriteLine(_ContractSheet._SheetName);
        }
        //public IEnumerable<Dictionary<string, string>> GetCommodity => 

        public bool IsValid => Valid();
    }
}
