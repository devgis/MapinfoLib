using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISLib.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        localhost.GISService service = new localhost.GISService();
        private void button1_Click(object sender, EventArgs e)
        {
            //String Result = GISLib.SearchClass.Instance.SearchByXY(113.23333, 23.16667);
            try
            {
                DateTime time1 = DateTime.Now;
                double x = Convert.ToDouble(textBox1.Text.Trim());
                double y = Convert.ToDouble(textBox2.Text.Trim());
                //String Result = GISLib.SearchClass.Instance.SearchNearRoad(x, y);
                string Result = service.SearchNearRoad(x, y);
                DateTime time2 = DateTime.Now;
                MessageBox.Show(Result + " 耗时:"+(time2-time1).TotalMilliseconds+"ms");
            }
            catch(Exception ex)
            {
                MessageBox.Show("发生错误;"+ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GISLib.SearchClass.Instance.Init();
        }
    }
}