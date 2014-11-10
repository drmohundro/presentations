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
            Async.InitializeSynchronizationContext(SynchronizationContext.Current);
        }


        #region The long running process... that we don't want to block our code

        /// <summary>
        /// Our really complicated, long running method
        /// </summary>
        /// <returns></returns>
        private static int CalculateRandomNumber()
        {
            var rnd = new Random();
            Thread.Sleep(500);
            return rnd.Next();
        }

        #endregion


        #region Blocking UI... and introducing the yield statement

        /// <summary>
        /// This call will block the UI thread... it also shows off the C# yield statement
        /// </summary>
        private void btnDoWorkNormal_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            var watch = new Stopwatch();
            watch.Start();

            // yield return is syntactic sugar for returning an enumerable - coroutine
            foreach (var rnd in GetRandomNumbers(5))
            {
                Debug.WriteLine(rnd);
                txtLog.AppendText(rnd.ToString() + Environment.NewLine);
            }

            watch.Stop();

            txtTotalTime.Text = watch.Elapsed.ToString();
        }

        /// <summary>
        /// Example of yield return here... C#'s implementation of coroutines
        /// 
        /// Just syntactic sugar for returning an enumerable... except that it "yields" control
        /// to the calling method before going to the next option. And it is up to the caller
        /// to ask for the next result. (note the order of writelines)
        /// 
        /// This is lazy evaluation works with LINQ (IQueryable)
        /// </summary>
        private static IEnumerable<int> GetRandomNumbers(int numberOfRandomsToReturn)
        {
            for (var i = 0; i < numberOfRandomsToReturn; i++)
            {
                Debug.WriteLine("before yield return");
                yield return CalculateRandomNumber();
                Debug.WriteLine("after yield return");
                Debug.WriteLine("");
            }

            Debug.WriteLine("Done!");
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
            var enumerator = GetRandomNumbersContinuation(5).GetEnumerator();

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
        private IEnumerable<IResult> GetRandomNumbersContinuation(int numberOfRandomsToReturn)
        {
            var watch = new Stopwatch();
            watch.Start();

            for (var i = 0; i < numberOfRandomsToReturn; i++)
            {
                yield return new AsyncResult<int>(
                    CalculateRandomNumber,
                    x => txtLog.AppendText(x.ToString() + Environment.NewLine),
                    ex => MessageBox.Show(ex.ToString()));
            }

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
            await GetRandomNumbersAsync(5);
        }


        /// <summary>
        /// Example of using async/await to add threading (non-blocking) to our app.
        /// 
        /// Can you see how these are basically doing the same thing?
        /// </summary>
        private async Task GetRandomNumbersAsync(int numberOfRandomsToReturn)
        {
            var watch = new Stopwatch();
            watch.Start();

            for (var i = 0; i < numberOfRandomsToReturn; i++)
            {
                var result = await Task.Run(() => CalculateRandomNumber());

                txtLog.AppendText(result.ToString() + Environment.NewLine);
            }

            watch.Stop();

            txtTotalTime.Text = watch.Elapsed.ToString();
        }

        #endregion
    }
}
