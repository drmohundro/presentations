using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AddSingleThread
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // NOTE WHAT VS DOES WHEN THE DEBUGGER IS ATTACHED
            var thread = new System.Threading.Thread(DoWork);
            thread.Start();

//            var thread = new System.Threading.Thread(DoWorkWithInvoke);
//            thread.Start();
//
//            var thread = new System.Threading.Thread(DoWorkWithBeginInvoke);
//            thread.Start();
//
//            var thread = new System.Threading.Thread(DoWorkPerformantlyWithBeginInvoke);
//            thread.Start();
        }

        private void DoWork()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i <= 100000; i++)
            {
                // NO, NO, NO, NO, NO... NEVER UDPATE UI THREAD FROM BACKGROUND THREAD
                txtCount.Text = i.ToString();
            }
            sw.Stop();

            // NO, NO, NO, NO, NO... NEVER UDPATE UI THREAD FROM BACKGROUND THREAD
            txtTotalTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void DoWorkWithInvoke()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i <= 100000; i++)
            {
                Invoke(new Action<object>(x =>
                {
                    txtCount.Text = x.ToString();
                }), i);
            }
            sw.Stop();

            Invoke(new Action<object>(x =>
            {
                txtTotalTime.Text = x.ToString();
            }), sw.ElapsedMilliseconds);
        }

        private void DoWorkWithBeginInvoke()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i <= 100000; i++)
            {
                BeginInvoke(new Action<object>(x =>
                {
                    txtCount.Text = x.ToString();
                }), i);
            }
            sw.Stop();

            BeginInvoke(new Action<object>(x =>
            {
                txtTotalTime.Text = x.ToString();
            }), sw.ElapsedMilliseconds);
        }

        private void DoWorkPerformantlyWithBeginInvoke()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i <= 100000; i++)
            {
                if (i % 500 == 0)
                {
                    this.BeginInvoke(
                                new Action<object>(
                                    x =>
                                    {
                                        txtCount.Text = x.ToString();
                                    }),
                        i);
                }
            }
            sw.Stop();

            this.BeginInvoke(new Action<object>(x =>
            {
                txtTotalTime.Text = x.ToString();
            }), sw.ElapsedMilliseconds);
        }
    }
}
