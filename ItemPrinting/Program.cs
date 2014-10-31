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
        public static string DecodeBarCode(PictureBox pb, string pathImage)
        {
            reader = new BarcodeReader();
            pb.Load(pathImage);
            Result result = reader.Decode((Bitmap)pb.Image);
            return result.Text;
        }
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

        ///<summary>

        ///解码

        ///</summary>

        ///<paramname="pictureBox1"></param>

        public static void Decode(PictureBox pictureBox1)
        {
            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pictureBox1.Image);
        }
    }
}