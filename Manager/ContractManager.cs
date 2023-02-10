using ClosedXML.Excel;
using ONEReader.Build;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager
{
    public class ContractManager
    {

        private ContractProperties properties;
        private ContractInfo contractInfo;
        public string CONTRACT_PATH;
        private List<string> XML_DATA; //TRIAL VARIABLE
        private DataSet set;
        public bool success;
        public ContractManager(string eff, string exp, string contId, string amdId, string path, Carrier carrier)
        {
            contractInfo = new ContractInfo(carrier.GetStringValue(), contId, amdId, eff, exp);
            CONTRACT_PATH = path;
            success = true;
            //CHECK OF CONTRACT CURRENTLY OPEN
            try
            {
                Stream s = File.Open(CONTRACT_PATH, FileMode.Open, FileAccess.Read, FileShare.None);
                s.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please close contract file.");
                success = false;   
                return;
            }

            if( carrier != Carrier.ONE)
            {
                MessageBox.Show("Currently not able to convert CMA.");
                return;
            }
            XML_DATA = new List<string>();
            properties = new ContractProperties(contractInfo, CONTRACT_PATH);
        }
        public ContractManager() { }
        public List<string> getContractSheetList()
        {
            return properties.sheetList();

        }
        public List<List<dynamic>> getCommodity()
        {
            IContractDetail<List<dynamic>> list = new CommodityDetails<List<dynamic>>();
            list.CompileDataTable(set);

            return (List<List<dynamic>>) list.details;
        }
        public void startExtraction(List<string> sheetToExtract)
        {

            if( properties.contractSheets.Count() == 0)
            {

                foreach (string sheet in sheetToExtract)
                {
                    properties.contractSheets.Add(sheet);
                }
            }
            foreach(IContractsheet contract in properties.contractSheets)
            {
                Console.WriteLine(contract.sheetName);

                Assemble assemble = new Assemble(properties.pattern, contract);
                assemble.ReadData();
                assemble.WriteData();

            }
        }

        public DataTable commodity { get; set; }
        public DataTable rates { get; set; }
        public DataTable arbs { get; set; }
    }
}
