using System.Threading;

namespace ContinuationExample
{
    internal class SlowApiCall
    {
        public static string ProcessPayment()
        {
            Thread.Sleep(2500);
            return "stuff from API";
        }
    }
}