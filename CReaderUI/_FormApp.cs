using CReaderUI.CustomClass;
using CReaderUI.FormControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CReaderUI
{
    public partial class _FormApp : Form
    {
        public _FormApp()
        {
            InitializeComponent();

            panelBody.Controls.Add(Dashboard.Instance);
            Dashboard.Instance.BringToFront();

        }

        private void addFile_Click(object sender, EventArgs e)
        {
            _FormDialog fileDialog = new _FormDialog(this);
            fileDialog.ShowDialog();
           
            if( fileDialog.dialogReturn == _FormDialog.DialogReturn.LOAD)
            {
                
                var fileManager = new FileManager();
                fileManager.SetContract(fileDialog.contract);
                if( fileManager.success)
                {
                    panelBody.Controls.Clear();
                    panelBody.Controls.Add(fileManager);
                }
                
                
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            _FormDonwloadDialog dlDialog = new _FormDonwloadDialog(this);
            dlDialog.ShowDialog();
            if( dlDialog.dialogReturn == _FormDonwloadDialog.DialogReturn.LOAD)
            {

            }

        }
    }
}
