using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using ZXing.Rendering;
using System.Text.RegularExpressions;
using System.IO;

using System.Xml;
using System.Xml.Serialization;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;

namespace ItemPrinting
{
    static class Program
    {
        public static EncodingOptions options = null;
        public static BarcodeWriter write = null;
        public static BarcodeReader reader = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        /// <summary>
        /// 生成QR码
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="contents"></param>
        public static void GenBarCode(PictureBox pb, string contents)
        {
            if (contents == string.Empty)
            {

                MessageBox.Show("输入内容不能为空！");

                return;

            }
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",//ISO-8859-1
                Width = pb.Width,
                Height = pb.Height
            };
            write = new BarcodeWriter();
            write.Format = BarcodeFormat.QR_CODE;
            write.Options = options;
            Bitmap bitmap = write.Write(contents);
            pb.Image = bitmap;
        }
        ///<summary>
        ///解码
        ///</summary>
        ///<paramname="pictureBox1"></param>
        public static string DecodeBarCode(PictureBox pb, string pathImage)
        {
            reader = new BarcodeReader();
            pb.Load(pathImage);
            Result result = reader.Decode((Bitmap)pb.Image);
            return result.Text;
        }
        ///<summary>
        ///解码
        ///</summary>
        ///<paramname="pictureBox1"></param>
        public static string DecodeBarCode(PictureBox pb)
        {
            reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pb.Image);
            return result.Text;
        }
        ///<summary>

        ///生成条形码

        ///</summary>

        ///<paramname="pictureBox1"></param>

        ///<paramname="Contents"></param>

        public static void CreateBarCode(PictureBox pictureBox1, string Contents)
        {

            Regex rg = new Regex("^[0-9]{12}$");

            if (!rg.IsMatch(Contents))
            {

                MessageBox.Show("本例子采用EAN_13编码，需要输入12位数字");

                return;

            }

            EncodingOptions options = null;
            BarcodeWriter writer = null;

            options = new EncodingOptions
            {
                Width = pictureBox1.Width,
                Height = pictureBox1.Height

            };

            writer = new BarcodeWriter();

            writer.Format = BarcodeFormat.ITF;

            writer.Options = options;

            Bitmap bitmap = writer.Write(Contents);

            pictureBox1.Image = bitmap;

        }
        ///<summary>

        ///生成二维码

        ///</summary>

        ///<paramname="pictureBox1"></param>

        ///<paramname="Contents"></param>

        public static void CreateQuickMark(PictureBox pictureBox1, string Contents)
        {

            if (Contents == string.Empty)
            {

                MessageBox.Show("输入内容不能为空！");

                return;

            }

            EncodingOptions options = null;

            BarcodeWriter writer = null;

            options = new QrCodeEncodingOptions
           {

               DisableECI = true,

               CharacterSet = "UTF-8",

               Width = pictureBox1.Width,

               Height = pictureBox1.Height

           };

            writer = new BarcodeWriter();

            writer.Format = BarcodeFormat.QR_CODE;

            writer.Options = options;


            Bitmap bitmap = writer.Write(Contents);

            pictureBox1.Image = bitmap;

        }

        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, ImageFormat.Gif);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
        // Strem轉換字節數組
        /// <summary>
        /// 在表报rdlc中。在报表主体中拖放一个image控件。
        /// 设置其Source为database，value=System.Convert.FromBase64String(Fields!barCode.Value)【byte[]数组时】，
        /// Base64String类型时直接绑定即可 // 关键之处
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            if (stream.CanRead)
            {
                stream.Read(bytes, 0, (int)stream.Length);
            }
            stream.Close();
            return bytes;

        }
        public static void initRV(ReportViewer rv)
        {
            rv.Reset();
            rv.LocalReport.DataSources.Clear();
        }
        public static void ShowReportViewer(ReportViewer rv, string rvname)
        {
            rv.LocalReport.DisplayName = rvname;
            rv.Refresh();
            rv.LocalReport.Refresh();
            rv.Visible = true;
        }

        public static void addDataSourceToReportViewer(ReportViewer rv, string reportDataSourceName, DataSet ds)
        {
            try
            {
                ReportDataSource rd = new ReportDataSource(reportDataSourceName, ds.Tables[0]);
                rv.LocalReport.DataSources.Add(rd);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


        }
        public static void addDataSourceToReportViewer(ReportViewer rv, string reportDataSourceName, DataTable dt)
        {
            try
            {
                ReportDataSource rd = new ReportDataSource(reportDataSourceName, dt);
                rv.LocalReport.DataSources.Add(rd);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


        }
        public static void initReportViewerLoadXMLfromPath(ReportViewer rv, string path)
        {
            //string tempxml = mydssqltemplate.Tables[0].Rows[0]["tempxml"].ToString();
            XmlDocument sourceDoc = new XmlDocument();
            //@"Reports\SO\siv_mstr_p.rdlc"
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            sourceDoc.Load(path);
            XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
            var m_rdl = new MemoryStream();
            serializer.Serialize(m_rdl, sourceDoc);
            if (m_rdl == null)
            {
                return;
            }
            rv.Reset();
            m_rdl.Position = 0;
            rv.LocalReport.DataSources.Clear();
            rv.LocalReport.LoadReportDefinition(m_rdl);
        }
        public static void ShowReportViewer(ReportViewer rv, string rvname, bool addprefxi)
        {
            string piefix = "Date" + DateTime.Today.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Today.Day.ToString() + "H" + DateTime.Now.Hour + "M" + DateTime.Now.Minute;
            rv.LocalReport.DisplayName = rvname + piefix;
            rv.RefreshReport();
            //rv.LocalReport.Refresh();
            rv.Visible = true;
        }

        ///////////////////////////////////////////
        //////////////end 
        ////////////////////////////////////////////////
    }
}