using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CReaderUI.FormModel;
using Manager;
using System.Threading;

namespace CReaderUI.FormControl
{
    public partial class FileManager : UserControl
    {
        private ContractModel _contract = new ContractModel();
        private ContractManager manager;
        public bool success;
        public FileManager()
        {
            InitializeComponent();

            gbData.Visible = false;
            gbLogs.Visible = false;
            success = true;
        }
        public void SetContract(ContractModel contract)
        {
            _contract = contract;
             manager = new ContractManager(_contract.effectiveDate, _contract.expirationDate, _contract.contractId, _contract.amendmentId, _contract.contractPath, _contract.carrier);
            this.success = manager.success;
            if (manager.success)
            {
                sheetlist.Items.AddRange(manager.getContractSheetList().ToArray());
               
            }

        }
        public void updateUI()
        {
            this.Invoke((MethodInvoker)delegate
            {

                btnExtract.Enabled = true;
                btnExtract.Text = "Extract";
                gbData.Visible = true;
                gbLogs.Visible = true;

            });
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            List<string> sheetToExtract = new List<string>();
            if( sheetlist.CheckedItems.Count > 0)
            {
                foreach (object ls in sheetlist.CheckedItems)
                {
                    sheetToExtract.Add(ls.ToString());
                }
            }
            else
            {
                MessageBox.Show(this, "Select sheet to extract.", "Error: Extract");
            }

            //DISABLE BUTTON
            btnExtract.Enabled = false;
            btnExtract.Text = "Extracting...";

            Action onCompleted = () =>
            {

                updateUI();

                //VALIDATION FOR BUTTON

                MessageBox.Show("Contract converted successfully.");


            };
            var ts = new Thread(() =>
            {
                try
                {
                    manager.startExtraction(sheetToExtract);
                }
                finally
                {
                    onCompleted();
                }
            });
            ts.Start();
        }
    }
}
