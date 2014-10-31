using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            dtRow["contents"] = Program.DecodeBarCode(pb);
            _barcodeDataTable.Rows.Add(dtRow);

            rv.LocalReport.DataSources.Clear();
            Program.addDataSourceToReportViewer(rv, "barcode", _barcodeDataTable);
            Program.ShowReportViewer(rv, textBox1.Text, true);
            rv.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.CreateBarCode(pictureBox1, textBox1.Text);
            initReportViewerForBarCode(pictureBox1, reportViewer1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请录入图像后再解码！");
                return;
            }
            MessageBox.Show(Program.DecodeBarCode(pictureBox1));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请录入图像后再解码！");
                return;
            }
            //else
            //{
            //    _doc = new DocementBase(pictureBox1.Image);
            //}
            //_doc.showPrintPreviewDialog();
            reportViewer1.PrintDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.GenBarCode(pictureBox1, textBox1.Text);
            initReportViewerForBarCode(pictureBox1, reportViewer1);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            reportViewer1.Width = this.Width - 10;
            reportViewer1.Height = this.Height - reportViewer1.Top - 10;
        }
    }
}