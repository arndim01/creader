using CReaderUI.FormModel;
using Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CReaderUI
{
    public partial class _FormDialog : Form
    {
        public DialogReturn dialogReturn;
        public ContractModel contract;
        public enum DialogReturn
        {
            NULL = 0,
            LOAD = 1
        }
        public _FormDialog(Form app)
        {
            InitializeComponent();
            dialogReturn = DialogReturn.NULL;
            contract = new ContractModel();

            dateEff.Value = DateTime.Today;
            dateExp.Value = DateTime.Today.AddDays(1);

            foreach( Carrier car in Enum.GetValues(typeof(Carrier)))
            {
                ddCarrier.AddItem(car.GetStringValue());
            }

        }

        private void loadContract_Click(object sender, EventArgs e)
        {
            if (ddCarrier.selectedIndex == -1)
            {

                MessageBox.Show(this, "Choose a valid carrier", "Error: Carrier");
                return;
            }
            
            if( dateEff.Value.ToShortDateString() == dateExp.Value.ToShortDateString() || dateEff.Value > dateExp.Value)
            {
                MessageBox.Show(this, "Invalidate date", "Error: Date");
                return;
            }
            
            if( txtContractId.text == "")
            {
                MessageBox.Show(this, "Invalid Contract Id", "Error: Contract Id");
                return;
            }
            if( txtContractAmd.text == "")
            {
                MessageBox.Show(this, "Invalid Amendment Id", "Error: Amendment Id");
                return;
            }
            
            if(String.IsNullOrWhiteSpace(contract.contractPath))
            {
                MessageBox.Show(this, "Select the contract file.", "Error: Choose File");
                return;
            }

            if( !CheckFileType(contract.contractPath))
            {
                MessageBox.Show(this, "Only accept excel file such as xlxs and xlsm.", "Error: File Format");
                return;
            }

            try
            {
                Stream s = File.Open(contract.contractPath, FileMode.Open, FileAccess.Read, FileShare.None);
                s.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please close contract file.");
                return;
            }

            contract.carrier = (Carrier) ddCarrier.selectedIndex;
            contract.effectiveDate = dateEff.Value.ToShortDateString();
            contract.expirationDate = dateExp.Value.ToShortDateString();
            contract.contractId = txtContractId.text.ToString();
            contract.amendmentId = txtContractAmd.text.ToString();

            dialogReturn = DialogReturn.LOAD;
            Close();

        }
        private bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".xlsx":
                    return true;
                case ".xlsm":
                    return true;
                default:
                    return false;
            }
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if( openFile.ShowDialog() != DialogResult.Cancel)
            {
                contract.contractPath = openFile.FileName;
                bunifuFlatButton2.Text = openFile.FileName;
            }
        }
    }
}
