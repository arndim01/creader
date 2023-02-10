namespace CReaderUI.FormControl
{
    partial class FileManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManager));
            this.sheetlist = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExtract = new Bunifu.Framework.UI.BunifuFlatButton();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnArbs = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnCommodity = new Bunifu.Framework.UI.BunifuTileButton();
            this.gbLogs = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.gbData.SuspendLayout();
            this.SuspendLayout();
            // 
            // sheetlist
            // 
            this.sheetlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sheetlist.CheckOnClick = true;
            this.sheetlist.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sheetlist.FormattingEnabled = true;
            this.sheetlist.Location = new System.Drawing.Point(6, 20);
            this.sheetlist.Name = "sheetlist";
            this.sheetlist.Size = new System.Drawing.Size(213, 198);
            this.sheetlist.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExtract);
            this.groupBox1.Controls.Add(this.sheetlist);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 263);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Tabs";
            // 
            // btnExtract
            // 
            this.btnExtract.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnExtract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.btnExtract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtract.BorderRadius = 0;
            this.btnExtract.ButtonText = "Extract";
            this.btnExtract.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExtract.DisabledColor = System.Drawing.Color.Gray;
            this.btnExtract.Iconcolor = System.Drawing.Color.Transparent;
            this.btnExtract.Iconimage = null;
            this.btnExtract.Iconimage_right = null;
            this.btnExtract.Iconimage_right_Selected = null;
            this.btnExtract.Iconimage_Selected = null;
            this.btnExtract.IconMarginLeft = 0;
            this.btnExtract.IconMarginRight = 0;
            this.btnExtract.IconRightVisible = true;
            this.btnExtract.IconRightZoom = 0D;
            this.btnExtract.IconVisible = true;
            this.btnExtract.IconZoom = 90D;
            this.btnExtract.IsTab = false;
            this.btnExtract.Location = new System.Drawing.Point(6, 225);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.btnExtract.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnExtract.OnHoverTextColor = System.Drawing.Color.White;
            this.btnExtract.selected = false;
            this.btnExtract.Size = new System.Drawing.Size(83, 33);
            this.btnExtract.TabIndex = 1;
            this.btnExtract.Text = "Extract";
            this.btnExtract.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExtract.Textcolor = System.Drawing.Color.White;
            this.btnExtract.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // gbData
            // 
            this.gbData.Controls.Add(this.bunifuTileButton1);
            this.gbData.Controls.Add(this.btnArbs);
            this.gbData.Controls.Add(this.btnCommodity);
            this.gbData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbData.ForeColor = System.Drawing.Color.DimGray;
            this.gbData.Location = new System.Drawing.Point(238, 3);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(409, 263);
            this.gbData.TabIndex = 2;
            this.gbData.TabStop = false;
            this.gbData.Text = "Extracted Data";
            this.gbData.Visible = false;
            // 
            // bunifuTileButton1
            // 
            this.bunifuTileButton1.BackColor = System.Drawing.Color.White;
            this.bunifuTileButton1.color = System.Drawing.Color.White;
            this.bunifuTileButton1.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.bunifuTileButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTileButton1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuTileButton1.ForeColor = System.Drawing.Color.Black;
            this.bunifuTileButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuTileButton1.Image")));
            this.bunifuTileButton1.ImagePosition = 14;
            this.bunifuTileButton1.ImageZoom = 50;
            this.bunifuTileButton1.LabelPosition = 29;
            this.bunifuTileButton1.LabelText = "Rates";
            this.bunifuTileButton1.Location = new System.Drawing.Point(160, 40);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(83, 84);
            this.bunifuTileButton1.TabIndex = 3;
            // 
            // btnArbs
            // 
            this.btnArbs.BackColor = System.Drawing.Color.White;
            this.btnArbs.color = System.Drawing.Color.White;
            this.btnArbs.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.btnArbs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnArbs.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArbs.ForeColor = System.Drawing.Color.Black;
            this.btnArbs.Image = ((System.Drawing.Image)(resources.GetObject("btnArbs.Image")));
            this.btnArbs.ImagePosition = 14;
            this.btnArbs.ImageZoom = 50;
            this.btnArbs.LabelPosition = 29;
            this.btnArbs.LabelText = "Arbitrary";
            this.btnArbs.Location = new System.Drawing.Point(282, 40);
            this.btnArbs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnArbs.Name = "btnArbs";
            this.btnArbs.Size = new System.Drawing.Size(83, 84);
            this.btnArbs.TabIndex = 2;
            // 
            // btnCommodity
            // 
            this.btnCommodity.BackColor = System.Drawing.Color.White;
            this.btnCommodity.color = System.Drawing.Color.White;
            this.btnCommodity.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(212)))));
            this.btnCommodity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCommodity.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommodity.ForeColor = System.Drawing.Color.Black;
            this.btnCommodity.Image = ((System.Drawing.Image)(resources.GetObject("btnCommodity.Image")));
            this.btnCommodity.ImagePosition = 14;
            this.btnCommodity.ImageZoom = 50;
            this.btnCommodity.LabelPosition = 29;
            this.btnCommodity.LabelText = "Commodity";
            this.btnCommodity.Location = new System.Drawing.Point(35, 40);
            this.btnCommodity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCommodity.Name = "btnCommodity";
            this.btnCommodity.Size = new System.Drawing.Size(79, 84);
            this.btnCommodity.TabIndex = 0;
            // 
            // gbLogs
            // 
            this.gbLogs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLogs.ForeColor = System.Drawing.Color.DimGray;
            this.gbLogs.Location = new System.Drawing.Point(7, 266);
            this.gbLogs.Name = "gbLogs";
            this.gbLogs.Size = new System.Drawing.Size(640, 125);
            this.gbLogs.TabIndex = 3;
            this.gbLogs.TabStop = false;
            this.gbLogs.Text = "Extract logs";
            this.gbLogs.Visible = false;
            // 
            // FileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbLogs);
            this.Controls.Add(this.gbData);
            this.Controls.Add(this.groupBox1);
            this.Name = "FileManager";
            this.Size = new System.Drawing.Size(656, 401);
            this.groupBox1.ResumeLayout(false);
            this.gbData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox sheetlist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbData;
        private System.Windows.Forms.GroupBox gbLogs;
        private Bunifu.Framework.UI.BunifuTileButton btnCommodity;
        private Bunifu.Framework.UI.BunifuTileButton btnArbs;
        private Bunifu.Framework.UI.BunifuTileButton bunifuTileButton1;
        private Bunifu.Framework.UI.BunifuFlatButton btnExtract;
    }
}
