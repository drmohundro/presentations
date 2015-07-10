using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class NamespaceItem
    {
        public string Namespace { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExecutionTimer.Options(
                showInfo: true
            );

            SynchronousAggregation();

            ParallelAggregationWithTpl();

            ParallelAggregationWithLinq();
        }

        static IEnumerable<string> FilesToRead => 
            Directory.EnumerateFiles(@"c:\dev", "*.cs", SearchOption.AllDirectories);

        /// <summary>
        /// The operation we need to run against every line from our history file
        /// </summary>
        static NamespaceItem ParseText(string line)
        {
            var match = Regex.Match(line, @"namespace (\w+)");

            if (!match.Success)
                return null;

            return new NamespaceItem { Namespace = match.Groups[1].Value };
        }

        private static void SynchronousAggregation()
        {
            using (ExecutionTimer.Start("SynchronousAggregation", blockResults: true))
            {
                var syncResults = new List<NamespaceItem>();
                foreach (var line in FilesToRead)
                {
                    syncResults.Add(ParseText(File.ReadAllText(line)));
                }
                Console.WriteLine("Results: {0}", syncResults.Count);
            }
        }

        private static void ParallelAggregationWithTpl()
        {
            using (ExecutionTimer.Start("Parallel.ForEach", blockResults: true))
            {
                var parallelResults = new List<NamespaceItem>();
                var parallelResultsLock = new object();
                Parallel.ForEach(
                    // the IEnumerable to enumerate over...
                    FilesToRead,

                    // initialization
                    () => new List<NamespaceItem>(),

                    // loop body
                    (line, loopState, partialResult) =>
                    {
                        partialResult.Add(ParseText(File.ReadAllText(line)));
                        return partialResult;
                    },

                    // local finally (good for aggregation)
                    (localPartialResult) =>
                    {
                        lock (parallelResultsLock)
                        {
                            parallelResults.AddRange(localPartialResult);
                        }
                    });

                Console.WriteLine("Results: {0}", parallelResults.Count);
            }
        }

        private static void ParallelAggregationWithLinq()
        {
            using (ExecutionTimer.Start("PLINQ (AsParallel)", blockResults: true))
            {
                var linqResults = FilesToRead
                    .AsParallel()
                    .Select(x => ParseText(File.ReadAllText(x)))
                    .ToList();
                Console.WriteLine("Results: {0}", linqResults.Count);
            }
        }
    }
}