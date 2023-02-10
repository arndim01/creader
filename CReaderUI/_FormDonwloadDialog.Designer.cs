namespace CReaderUI
{
    partial class _FormDonwloadDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_FormDonwloadDialog));
            this.txtuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dlContract = new Bunifu.Framework.UI.BunifuFlatButton();
            this.SuspendLayout();
            // 
            // txtuid
            // 
            this.txtuid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuid.Location = new System.Drawing.Point(103, 28);
            this.txtuid.Name = "txtuid";
            this.txtuid.Size = new System.Drawing.Size(237, 29);
            this.txtuid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contract UID:";
            // 
            // dlContract
            // 
            this.dlContract.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.dlContract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.dlContract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dlContract.BorderRadius = 0;
            this.dlContract.ButtonText = "Download Contract";
            this.dlContract.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dlContract.DisabledColor = System.Drawing.Color.Gray;
            this.dlContract.Iconcolor = System.Drawing.Color.Transparent;
            this.dlContract.Iconimage = null;
            this.dlContract.Iconimage_right = null;
            this.dlContract.Iconimage_right_Selected = null;
            this.dlContract.Iconimage_Selected = null;
            this.dlContract.IconMarginLeft = 0;
            this.dlContract.IconMarginRight = 0;
            this.dlContract.IconRightVisible = true;
            this.dlContract.IconRightZoom = 0D;
            this.dlContract.IconVisible = true;
            this.dlContract.IconZoom = 50D;
            this.dlContract.IsTab = false;
            this.dlContract.Location = new System.Drawing.Point(12, 83);
            this.dlContract.Name = "dlContract";
            this.dlContract.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.dlContract.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(177)))), ((int)(((byte)(241)))));
            this.dlContract.OnHoverTextColor = System.Drawing.Color.White;
            this.dlContract.selected = false;
            this.dlContract.Size = new System.Drawing.Size(135, 28);
            this.dlContract.TabIndex = 28;
            this.dlContract.Text = "Download Contract";
            this.dlContract.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dlContract.Textcolor = System.Drawing.Color.White;
            this.dlContract.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dlContract.Click += new System.EventHandler(this.dlContract_Click);
            // 
            // _FormDonwloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(352, 123);
            this.Controls.Add(this.dlContract);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtuid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_FormDonwloadDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtuid;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuFlatButton dlContract;
    }
}