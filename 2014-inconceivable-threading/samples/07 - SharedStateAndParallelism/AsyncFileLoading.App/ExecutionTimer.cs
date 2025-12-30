using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApplication1
{
    internal class ExecutionTimer : IDisposable
    {
        private string Name { get; set; }
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
                RandomizeConsoleColor();
                if (ShouldLog)
                {
                    Console.WriteLine("--- {0} START", Name);
                }
            }

            _stopwatch.Start();
        }

        private void RandomizeConsoleColor()
        {
            if (!ColorizeOutput) return;

            _lastColor = Console.ForegroundColor;

            var done = false;
            do
            {
                if (currentColorIndex >= _logColors.Count)
                    currentColorIndex = 0;

                Console.ForegroundColor = _logColors[currentColorIndex];
                if (Console.ForegroundColor != ConsoleColor.Black)
                {
                    done = true;
                }

                currentColorIndex++;
            }
            while (!done);
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
            if (BlockResults && ColorizeOutput)
            {
                Console.ForegroundColor = _lastColor;
                Console.WriteLine("");
            }
        }

        private bool ShouldLog
        {
            get { return !InfoOnly || ShowInfo; }
        }

        public static void Options(bool colorizeOutput = false, bool showInfo = false)
        {
            ColorizeOutput = colorizeOutput;
            ShowInfo = showInfo;
        }

        private static bool ColorizeOutput { get; set; }
        private static bool ShowInfo { get; set; }
        private ConsoleColor _lastColor;
        private static int currentColorIndex = 0;

        private readonly List<ConsoleColor> _logColors = new List<ConsoleColor>(new[] { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Gray });
    }
}