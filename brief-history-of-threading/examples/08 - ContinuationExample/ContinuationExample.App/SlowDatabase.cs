using System.Threading;

namespace ContinuationExample
{
    internal class SlowDatabase
    {
        public static string LogCharge(string chargeResults)
        {
            Thread.Sleep(2500);
            return "Charge Saved!";
        }
    }
}