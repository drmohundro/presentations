using System;
using System.Windows.Forms;

namespace IAsyncResultExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // the inputs
            int x = 2;

            // the "thread" or work to be "BeginInvoked"
            var dlg = new Func<int, int>(
                param =>
                {
                    // put in a sleep just for fun...
                    System.Threading.Thread.Sleep(500);

                    // note that, while we COULD have referenced "x" here, we are NOT...
                    // pretend this lives somewhere else... or is even a web request or something.
                    return param * 2;
                });

            // "BeginInvoke" will spawn a new ThreadPool thread to do the work for us
            var iar = dlg.BeginInvoke(
                // this corresponds to the "param" in the delegate above
                x,

                // who gets called when the work is completed
                Callback,

                // "AsyncState" - used to pass additional information around
                // By convention, the convention was to usually use an object array
                // I'd recommend using a Tuple now for more "strong typed ness"
                // - first parameter here is the actual delegate (lambda)
                // - second parameter here is the input (why? you'll see in a little bit...)
                // Alternatively, you can use a custom object here... depends on the consumer as far as which you'd want to use
                new Tuple<Func<int, int>, int>(dlg, x));
        }

        private void Callback(IAsyncResult ar)
        {
            // ar.AsyncState corresponds to our object array above
            var asyncState = ar.AsyncState as Tuple<Func<int, int>, int>;

            // the first item is our delegate (see Func<int, int> as before)
            var dlg = asyncState.Item1;

            // the second item is the "x", or our input
            var x = asyncState.Item2;

            // how do you actually get the result of our delegate?
            // you HAVE to "EndInvoke" to get the result
            // note that, if this didn't have a return result, you should still ALWAYS EndInvoke to clean everything up
            var result = dlg.EndInvoke(ar);

            // the callback will be on another ThreadPool thread, so you have to marshall back to the UI thread with your results
            // (like from our last session)
            Invoke(new Action(() => MessageBox.Show("The result is " + result.ToString())));
        }
    }
}
