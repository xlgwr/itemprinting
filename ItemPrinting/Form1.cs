using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ItemPrinting
{
    public partial class Form1 : Form
    {
        private DocementBase _doc;
        public DataTable _barcodeDataTable;  //自定义的数据集

        public Form1()
        {
            InitializeComponent();
            initDataTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
            textBox1.Text = DateTime.Now.ToString("yyyMMddhhmm").Substring(0, 12);
        }
        void initDataTable()
        {
            _barcodeDataTable = new DataTable();
            DataColumn dc = new DataColumn("ProID");
            DataColumn dc2 = new DataColumn("barCode");
            DataColumn dc3 = new DataColumn("date");
            DataColumn dc4 = new DataColumn("contents");
            _barcodeDataTable.Columns.Add(dc);
            _barcodeDataTable.Columns.Add(dc2);
            _barcodeDataTable.Columns.Add(dc3);
            _barcodeDataTable.Columns.Add(dc4);
        }
        public void initReportViewerForBarCode(PictureBox pb, ReportViewer rv)
        {
            Bitmap bp = (Bitmap)pb.Image;
            byte[] imgBytes = Program.BitmapToBytes(bp);

            _barcodeDataTable.Clear();
            DataRow dtRow = _barcodeDataTable.NewRow();
            dtRow["ProID"] = "BD11-12/800-16D";
            dtRow["barCode"] = Convert.ToBase64String(imgBytes);  //存放前先转码。关键之处。
            dtRow["date"] = DateTime.Now;
            //dtRow["contents"] = Program.DecodeBarCode(pb);
            _barcodeDataTable.Rows.Add(dtRow);

            Program.initReportViewerLoadXMLfromPath(reportViewer1, "Report1.rdlc");
            //rv.LocalReport.DataSources.Clear();
            Program.addDataSourceToReportViewer(rv, "barcode", _barcodeDataTable);
            Program.ShowReportViewer(rv, textBox1.Text, true);
            rv.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.CreateBarCode(pictureBox1, 99, 45, textBox1.Text);
            initReportViewerForBarCode(pictureBox1, reportViewer1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请生成图像后再打印！");
                return;
            }
            MessageBox.Show(Program.DecodeBarCode(pictureBox1));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请生成图像后再导出！");
                return;
            }
            string pathname = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\";
            string allpdfpath;
            lblmsg.Text = Program.ExportTypeForReportViewer(reportViewer1, "PDF", textBox1.Text + ".pdf",out allpdfpath);
           // Program.pdfPrintProcess(allpdfpath);
           Program.pdfPrintProcess2(allpdfpath);
           //Program.pdfPrintAdobe(allpdfpath,axAcroPDF1);
           // Program.pdfPrintiTextSharp(allpdfpath,pathname+"towrite.pdf",true);
           textBox1.SelectAll();
           textBox1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.GenBarCode(pictureBox1, 99, 99, textBox1.Text);
            initReportViewerForBarCode(pictureBox1, reportViewer1);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            reportViewer1.Width = this.Width - 15;
            reportViewer1.Height = this.Height - reportViewer1.Top - 15;
        }
    }
}