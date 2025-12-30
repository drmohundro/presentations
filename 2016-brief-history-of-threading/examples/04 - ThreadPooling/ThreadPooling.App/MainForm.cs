using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ThreadPooling
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(DoWork);
        }

        private void DoWork(object state)
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i <= 100000; i++)
            {
                if (i % 500 == 0)
                {
                    // just so we can see it more easily...
                    Thread.Sleep(5);

                    // have to use Control.BeginInvoke again here...
                    BeginInvoke(
                        new Action<object>(
                            x =>
                            {
                                txtCount.Text = x.ToString();
                            }),
                        i);
                }
            }
            sw.Stop();

            // have to use Control.BeginInvoke again here...
            BeginInvoke(new Action<object>(x =>
            {
                txtTotalTime.Text = x.ToString();
            }), sw.ElapsedMilliseconds);
        }
    }
}
