using System;
using System.Diagnostics;

namespace ConsoleApplication1
{
    internal class ExecutionTimer : IDisposable
    {
        private string Name { get; }
        public bool BlockResults { get; set; }
        public bool InfoOnly { get; set; }
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private ExecutionTimer(string name, bool blockResults, bool infoOnly)
        {
            Name = name;
            BlockResults = blockResults;
            InfoOnly = infoOnly;

            if (BlockResults)
            {
                if (ShouldLog)
                {
                    Console.WriteLine("--- {0} START", Name);
                }
            }

            _stopwatch.Start();
        }

        public static ExecutionTimer Start(string name, bool blockResults = false, bool infoOnly = false)
        {
            return new ExecutionTimer(name, blockResults, infoOnly);
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            if (!ShouldLog) return;

            Console.WriteLine("--- {0} [{1}]", Name, _stopwatch.Elapsed);
            if (BlockResults)
            {
                Console.WriteLine("");
            }
        }

        private bool ShouldLog => !InfoOnly || ShowInfo;

        public static void Options(bool showInfo = false)
        {
            ShowInfo = showInfo;
        }

        private static bool ShowInfo { get; set; }
    }
}