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
using System.Drawing.Printing;
using System.Diagnostics;


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
            Application.Run(new frm0ItemPrinting());
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
        /// <summary>
        /// 生成QR码
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="contents"></param>
        public static void GenBarCode(PictureBox pb, int width, int height, string contents)
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
                Width = width,
                Height = height
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

        ///生成条形码

        ///</summary>

        ///<paramname="pictureBox1"></param>

        ///<paramname="Contents"></param>

        public static void CreateBarCode(PictureBox pictureBox1, int width, int height, string Contents)
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
                Width = width,
                Height = height

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
            path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + path;
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
        /// <summary>
        /// Excel、PDF 和 Image。
        /// </summary>
        /// <param name="rv"></param>
        /// <param name="namePath"></param>
        /// <param name="strtype"></param>
        public static string ExportTypeForReportViewer(ReportViewer rv, string strtype, string namePath, out string fullpath)
        {
            Warning[] Warnings;
            string[] strStreamIds;
            string strMimeType;
            string strEncoding;
            string strFileNameExtension;
            string piefix = "D" + DateTime.Today.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Today.Day.ToString() + "H" + DateTime.Now.Hour + "M" + DateTime.Now.Minute + "-";

            byte[] bytes = rv.LocalReport.Render(strtype, null, out strMimeType, out strEncoding, out strFileNameExtension, out strStreamIds, out Warnings);

            string pathname = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PDF\";
            string strFilePath = @pathname + @piefix + @namePath;// @"D:\report.xls";

            using (System.IO.FileStream fs = new FileStream(strFilePath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);

                fullpath = strFilePath;
                return "生成[" + strFilePath + "]OK";
            }


        }
        //打印方法1慢

        public static void pdfPrintProcess(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            string pdfPath = filePath;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //不现实调用程序窗口,但是对于某些应用无效
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //采用操作系统自动识别的模式
            p.StartInfo.UseShellExecute = true;

            //要打印的文件路径，可以是WORD,EXCEL,PDF,TXT等等
            p.StartInfo.FileName = pdfPath;

            //指定执行的动作，是打印，即print，打开是 open
            p.StartInfo.Verb = "print";

            //开始
            p.Start();


        }
        public static void pdfPrintProcess2(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            //string SavePath = @"d:\temp.pdf";
            //string printerName = "Apollo P2200";
            //string pdfArguments = string.Format(" /p " + "\"" + SavePath + "\"" + "  \"" + printerName + "\"");
            string pdfArguments = string.Format(" /p " + "\"" + filePath + "\"");// -n 1 -z 100
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "zfr.exe";//需要启动的程序名  
            //MessageBox.Show(pdfArguments);
            p.StartInfo.Arguments = pdfArguments;
            p.Start();//启动  
            //    pdfProcess.WaitForExit();


        }
        public static string vs_SplitStr(string vStr, Int16 intP, string vSplit) /*vSplit=":"   intP=2*/
        {
            string retStr = "";
            if (vStr.Contains(vSplit)) { vStr = vStr.Replace(vSplit, ""); }
            vStr = vStr.Trim();
            if (vStr.Length != 12) { return "2nd Label长度不对"; }

            if (intP == 0) { return retStr; }

            if (vStr.Length % intP == 0)    /*求余数*/
            {
                for (Int16 i = 0; i < vStr.Length / intP; i++)
                {
                    retStr = (retStr.Length == 0 ? vStr.Substring(0, intP) : retStr + vSplit + vStr.Substring(intP * i, intP));
                }
            }
            return retStr;
        }
        //public static void pdfPrintAdobe(string filePath, AxAcroPDFLib.AxAcroPDF axAcroPDF1)
        //{
        //    if (string.IsNullOrEmpty(filePath))
        //    {
        //        return;
        //    }
        //    axAcroPDF1.LoadFile(filePath);
        //    axAcroPDF1.setShowToolbar(false);

        //    axAcroPDF1.LoadFile(filePath);
        //    axAcroPDF1.printAll();


        //}
       
        ///////////////////////////////////////////
        //////////////end 
        ////////////////////////////////////////////////
    }
}