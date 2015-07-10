using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuationExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // initialize our async helper....
            AsyncHelper.InitializeSynchronizationContext(SynchronizationContext.Current);
        }


        #region Blocking UI

        /// <summary>
        /// This call will block the UI thread... it also shows off the C# yield statement
        /// </summary>
        private void btnDoWorkNormal_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            var watch = new Stopwatch();
            watch.Start();

            // let user know we're working...
            statusLabel.Text = "Processing payment...";

            // call slow API
            var chargeResults = SlowApiCall.ProcessPayment();

            // update UI with results
            txtLog.AppendText($"Process Payment Results: {chargeResults}{Environment.NewLine}");

            // call database with results from API
            var dbResults = SlowDatabase.LogCharge(chargeResults);
            
            // update UI
            statusLabel.Text = "Saving to database...";
            txtLog.AppendText($"Database Results: {dbResults}{Environment.NewLine}");

            statusLabel.Text = "DONE!";
            txtLog.AppendText($"Done!{Environment.NewLine}{Environment.NewLine}");

            watch.Stop();

            txtTotalTime.Text = watch.Elapsed.ToString();
        }

        #endregion


        #region Non blocking UI... with Tasks

        private void btnNonBlockedUIManual_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            var watch = new Stopwatch();
            watch.Start();

            // let user know we're working...
            statusLabel.Text = "Processing payment...";

            // call slow API
            Task.Run(() => SlowApiCall.ProcessPayment())
                .ContinueWith(prevTask =>
                {
                    var chargeResults = prevTask.Result;

                    AsyncHelper.BeginOnUIThread(() =>
                    {
                        // update UI with results
                        txtLog.AppendText($"Process Payment Results: {chargeResults}{Environment.NewLine}");
                    });

                    // call database with results from API
                    return SlowDatabase.LogCharge(chargeResults);

                }).ContinueWith(prevTask =>
                {
                    // call database with results from API
                    var dbResults = prevTask.Result;

                    AsyncHelper.BeginOnUIThread(() =>
                    {
                        // update UI
                        statusLabel.Text = "Saving to database...";
                        txtLog.AppendText($"Database Results: {dbResults}{Environment.NewLine}");

                        statusLabel.Text = "DONE!";
                        txtLog.AppendText($"Done!{Environment.NewLine}{Environment.NewLine}");

                        watch.Stop();

                        txtTotalTime.Text = watch.Elapsed.ToString();
                    });
                });
        }

        #endregion


        #region Non blocking UI... with manual continuations

        /// <summary>
        /// This call will NOT block the UI thread.
        /// </summary>
        private void btnDoWorkContinuation_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            // manual enumerator
            var enumerator = DoWorkWithManualContinuations().GetEnumerator();

            ExecuteContinuation(enumerator);
        }


        /// <summary>
        /// Recursively call continuations... it doesn't care about threads or not
        /// </summary>
        private static void ExecuteContinuation(IEnumerator<IResult> enumerator)
        {
            if (!enumerator.MoveNext()) return;

            var result = enumerator.Current;
            result.Completed = () => ExecuteContinuation(enumerator);
            result.Execute();
        }


        /// <summary>
        /// Example of using continuations (with coroutines) to add threading (non-blocking) to our app.
        /// 
        /// Still using yield return, but our caller doesn't just enumerate the results
        /// because we don't want to block the UI. However, we're not having to use callbacks here.
        /// 
        /// Why not?
        /// 
        /// See AsyncResult code + the calling code. This ideas was popularized in .NET by the Caliburn
        /// library (which Deployer uses).
        /// </summary>
        private IEnumerable<IResult> DoWorkWithManualContinuations()
        {
            txtLog.Clear();

            var watch = new Stopwatch();
            watch.Start();

            // let user know we're working...
            statusLabel.Text = "Processing payment...";

            string chargeResults = null;

            // call slow API
            yield return new AsyncResult<string>(
                action: SlowApiCall.ProcessPayment,
                callback: x => chargeResults = x,
                errorCallback: ex => MessageBox.Show(ex.ToString()));

            // update UI with results
            txtLog.AppendText($"Process Payment Results: {chargeResults}{Environment.NewLine}");

            string dbResults = null;

            // call database with results from API
            yield return new AsyncResult<string>(
                action: () => SlowDatabase.LogCharge(chargeResults),
                callback: x => dbResults = x,
                errorCallback: ex => MessageBox.Show(ex.ToString()));
            
            // update UI
            statusLabel.Text = "Saving to database...";
            txtLog.AppendText($"Database Results: {dbResults}{Environment.NewLine}");

            statusLabel.Text = "DONE!";
            txtLog.AppendText($"Done!{Environment.NewLine}{Environment.NewLine}");

            watch.Stop();

            txtTotalTime.Text = watch.Elapsed.ToString();
        }

        #endregion


        #region Non blocking UI... with async/await

        /// <summary>
        /// C# does this for us natively now. Awesome.
        /// </summary>
        private async void btnDoWorkAsync_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            await DoWorkWithAsync();
        }


        /// <summary>
        /// Example of using async/await to add threading (non-blocking) to our app.
        /// 
        /// Can you see how these are basically doing the same thing?
        /// </summary>
        private async Task DoWorkWithAsync()
        {
            txtLog.Clear();

            var watch = new Stopwatch();
            watch.Start();

            // let user know we're working...
            statusLabel.Text = "Processing payment...";

            // call slow API
            var chargeResults = await Task.Run(() => SlowApiCall.ProcessPayment());

            // update UI with results
            txtLog.AppendText($"Process Payment Results: {chargeResults}{Environment.NewLine}");

            // call database with results from API
            var dbResults = await Task.Run(() => SlowDatabase.LogCharge(chargeResults));
            
            // update UI
            statusLabel.Text = "Saving to database...";
            txtLog.AppendText($"Database Results: {dbResults}{Environment.NewLine}");

            statusLabel.Text = "DONE!";
            txtLog.AppendText($"Done!{Environment.NewLine}{Environment.NewLine}");

            watch.Stop();

            txtTotalTime.Text = watch.Elapsed.ToString();
        }

        #endregion
    }
}
