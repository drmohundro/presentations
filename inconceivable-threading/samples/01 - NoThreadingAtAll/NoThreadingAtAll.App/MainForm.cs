using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NoThreadingAtAll
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoWork();
        }

        private void DoWork()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i <= 100000; i++)
            {
                txtCount.Text = i.ToString();
            }
            sw.Stop();

            txtTotalTime.Text = sw.ElapsedMilliseconds.ToString();
        }
    }
}
