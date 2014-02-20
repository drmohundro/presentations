using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace BackgroundWorker {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (var worker = new System.ComponentModel.BackgroundWorker()) {
                worker.WorkerReportsProgress = true;

                worker.DoWork += DoWork;
                worker.ProgressChanged += (o, args) => {
                    txtCount.Text = args.UserState.ToString();
                };
                worker.RunWorkerCompleted += (o, args) => {
                    txtTotalTime.Text = args.Result.ToString();
                };

                worker.RunWorkerAsync();
            }
        }

        private void DoWork(object sender, DoWorkEventArgs args) {
            var worker = sender as System.ComponentModel.BackgroundWorker;

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i <= 100000; i++) {
                if (i % 500 == 0) {
                    // just so we can see it more easily...
                    Thread.Sleep(5);
                    worker.ReportProgress(i / 100000, i);
                }
            }
            sw.Stop();

            args.Result = sw.ElapsedMilliseconds;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {

        }
    }
}
