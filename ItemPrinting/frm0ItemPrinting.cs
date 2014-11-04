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
    public partial class frm0ItemPrinting : Form
    {
        private DocementBase _doc;
        public DataTable _barcodeDataTable;  //自定义的数据集
        public string _allpdfpath;

        public frm0ItemPrinting()
        {
            InitializeComponent();
            initDataTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
            //textBox1.Text = DateTime.Now.ToString("yyyMMddhhmm").Substring(0, 12);
            textBox1.Text="eo-RT100(S1),V2SA3783310027,0025dcb0d9a8,0025dcb0d9a9,0025dcb0d9aa,0025dcb0d9ab,eoRT-2b0d9aa-g,eoRT-2b0d9ab-a,a1a080a3122511,42475347,eoRT-2b0d9aa-gw,eoRT-2b0d9ab-aw,845555203c610a";
        }
        void initDataTable()
        {
            _barcodeDataTable = new DataTable();
            DataColumn dc = new DataColumn("ProID");
            DataColumn dc2 = new DataColumn("barCode");
            DataColumn dc3 = new DataColumn("date");
            DataColumn dc4 = new DataColumn("contents");
            DataColumn dc5 = new DataColumn("ssid1_2_4G");
            DataColumn dc6 = new DataColumn("ssid1_5G");
            DataColumn dc7 = new DataColumn("AES1");
            DataColumn dc8 = new DataColumn("PIN");
            DataColumn dc9 = new DataColumn("ssid2_2_4G");
            DataColumn dc10 = new DataColumn("ssid2_5G");
            DataColumn dc11 = new DataColumn("AES2");
            DataColumn dc12 = new DataColumn("name");
            DataColumn dc13 = new DataColumn("name2");
            DataColumn dc14 = new DataColumn("name3");
            DataColumn dc15 = new DataColumn("lan_mac");
            DataColumn dc16 = new DataColumn("wan_mac");
            DataColumn dc17 = new DataColumn("wifi_mac_2_4");
            DataColumn dc18 = new DataColumn("wifi_mac_5g");

            _barcodeDataTable.Columns.Add(dc);
            _barcodeDataTable.Columns.Add(dc2);
            _barcodeDataTable.Columns.Add(dc3);
            _barcodeDataTable.Columns.Add(dc4);
            _barcodeDataTable.Columns.Add(dc5);
            _barcodeDataTable.Columns.Add(dc6);
            _barcodeDataTable.Columns.Add(dc7);
            _barcodeDataTable.Columns.Add(dc8);
            _barcodeDataTable.Columns.Add(dc9);
            _barcodeDataTable.Columns.Add(dc10);
            _barcodeDataTable.Columns.Add(dc11);
            _barcodeDataTable.Columns.Add(dc12);
            _barcodeDataTable.Columns.Add(dc13);
            _barcodeDataTable.Columns.Add(dc14);
            _barcodeDataTable.Columns.Add(dc15);
            _barcodeDataTable.Columns.Add(dc16);
            _barcodeDataTable.Columns.Add(dc17);
            _barcodeDataTable.Columns.Add(dc18);
        }
        void initDataToDataTable(string sn,PictureBox pb)
        {
            _barcodeDataTable.Clear();

            string[] arr = sn.Split(','); //textBox1.Text.Trim().Split(',');
            if (arr.Length < 12)
            {
                lblmsg.Text = "条码不符合要求，请重新输入。";
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }
            Bitmap bp = (Bitmap)pb.Image;
            byte[] imgBytes = Program.BitmapToBytes(bp);          

            DataRow dtRow = _barcodeDataTable.NewRow();
            dtRow["ProID"] = "BD11-12/800-16D";
            dtRow["barCode"] = Convert.ToBase64String(imgBytes);  //存放前先转码。关键之处。
            dtRow["date"] = DateTime.Now;
            //dtRow["contents"] = Program.DecodeBarCode(pb);
            dtRow["ssid1_2_4G"] = arr[6].ToString().Trim().ToLower().Replace("rt", "RT");
            dtRow["ssid1_5G"] = arr[7].ToString().Trim().ToLower().Replace("rt", "RT"); 
            dtRow["AES1"] = arr[8];
            dtRow["PIN"] = arr[9];
            dtRow["ssid2_2_4G"] = arr[10];
            dtRow["ssid2_5G"] = arr[11];
            dtRow["AES2"] = arr[12];
            dtRow["name"] = "eo光多機能ルーター";
            dtRow["name2"] = arr[0];
            dtRow["name3"] = arr[1];
            dtRow["lan_mac"] = Program.vs_SplitStr(arr[2],2,":");
            dtRow["wan_mac"] = Program.vs_SplitStr(arr[3], 2, ":");
            dtRow["wifi_mac_2_4"] = Program.vs_SplitStr(arr[4], 2, ":");
            dtRow["wifi_mac_5g"] = Program.vs_SplitStr(arr[5], 2, ":");



            _barcodeDataTable.Rows.Add(dtRow);
        }
        public void initReportViewerForBarCode(ReportViewer rv,DataTable dt)
        {
           
            Program.initReportViewerLoadXMLfromPath(reportViewer1, "Report1.rdlc");
            //rv.LocalReport.DataSources.Clear();
            Program.addDataSourceToReportViewer(rv, "barcode", dt);
            Program.ShowReportViewer(rv, textBox1.Text, true);
            rv.RefreshReport();
        }

        private void btnGentiaoma_Click(object sender, EventArgs e)
        {
            Program.CreateBarCode(pictureBox1, 99, 45, textBox1.Text);

            initDataToDataTable(textBox1.Text.Trim(), pictureBox1);
            initReportViewerForBarCode(reportViewer1,_barcodeDataTable);
        }

        private void btnDecodeBar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请生成图像后再打印！");
                return;
            }
            MessageBox.Show(Program.DecodeBarCode(pictureBox1));
        }

        private void btn5Print_Click(object sender, EventArgs e)
        {
           
            Program.GenBarCode(pictureBox1, 113, 113, textBox1.Text);
           
            initDataToDataTable(textBox1.Text.Trim(), pictureBox1);

            initReportViewerForBarCode(reportViewer1,_barcodeDataTable);

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请生成图像后再导出！");
                return;
            }
            string pathname = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\";            
            lblmsg.Text = Program.ExportTypeForReportViewer(reportViewer1, "PDF", textBox1.Text.Substring(0,12) + ".pdf",out _allpdfpath);
           // Program.pdfPrintProcess(allpdfpath);
           Program.pdfPrintProcess2(_allpdfpath);
           //Program.pdfPrintAdobe(allpdfpath,axAcroPDF1);
           // Program.pdfPrintiTextSharp(allpdfpath,pathname+"towrite.pdf",true);
           textBox1.SelectAll();
           textBox1.Focus();

        }

        private void btn2QRcode_Click(object sender, EventArgs e)
        {
            Program.GenBarCode(pictureBox1, 113, 113, textBox1.Text);


            initDataToDataTable(textBox1.Text.Trim(), pictureBox1);
            initReportViewerForBarCode(reportViewer1,_barcodeDataTable);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            reportViewer1.Width = this.Width - 15;
            reportViewer1.Height = this.Height - reportViewer1.Top - 15;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn5Print_Click(sender, e);
            }
        }

        private void lblmsg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_allpdfpath))
            {
                
            }
        }
    }
}