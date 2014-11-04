using ItemPrinting.DS;
namespace ItemPrinting
{
    partial class frm0ItemPrinting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm0ItemPrinting));
            this.barcodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barcode = new ItemPrinting.DS.barcode();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn5Print = new System.Windows.Forms.Button();
            this.lblmsg = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.barcodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // barcodeBindingSource
            // 
            this.barcodeBindingSource.DataMember = "barcode";
            this.barcodeBindingSource.DataSource = this.barcode;
            // 
            // barcode
            // 
            this.barcode.DataSetName = "barcode";
            this.barcode.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "barcode";
            reportDataSource1.Value = this.barcodeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ItemPrinting.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 145);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 211);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(12, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(543, 55);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "eo-RT100(S1),V2SA3783310068,0025dcb0da4c,0025dcb0da4d,0025dcb0da4e,0025dcb0da4f,e" +
    "oRT-2b0da4e-g,eoRT-2b0da4f-a,0df03dc3734b14,13440381,eoRT-2b0da4e-gw,eoRT-2b0da4" +
    "f-aw,29d3fc3a6628a4";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(561, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btn5Print
            // 
            this.btn5Print.Location = new System.Drawing.Point(169, 75);
            this.btn5Print.Name = "btn5Print";
            this.btn5Print.Size = new System.Drawing.Size(103, 33);
            this.btn5Print.TabIndex = 5;
            this.btn5Print.Text = "打 印";
            this.btn5Print.UseVisualStyleBackColor = true;
            this.btn5Print.Click += new System.EventHandler(this.btn5Print_Click);
            // 
            // lblmsg
            // 
            this.lblmsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblmsg.Font = new System.Drawing.Font("SimSun", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(0, 111);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(682, 31);
            this.lblmsg.TabIndex = 7;
            this.lblmsg.Click += new System.EventHandler(this.lblmsg_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "二维码预览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn2QRcode_Click);
            // 
            // frm0ItemPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 356);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn5Print);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm0ItemPrinting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemPrinting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barcodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.BindingSource barcodeBindingSource;
        private barcode barcode;
        private System.Windows.Forms.Button btn5Print;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Button button1;
    }
}

