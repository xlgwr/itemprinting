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

        public Form1()
        {
            InitializeComponent();
        }
        private DocementBase _doc;

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
            textBox1.Text = DateTime.Now.ToString("yyyMMddhhmm").Substring(0, 12);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.CreateBarCode(pictureBox1, textBox1.Text);
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
            else
            {
                _doc = new DocementBase(pictureBox1.Image);
            }
            _doc.showPrintPreviewDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.GenBarCode(pictureBox1, textBox1.Text);
        }
    }
}