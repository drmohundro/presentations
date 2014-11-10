using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class HistoryItem
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime DateAccessed { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Url);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExecutionTimer.Options(
                colorizeOutput: true,
                showInfo: false
            );

            // -------------- Synchronous Aggregation ----------------
            SynchronousAggregation();

            // -------------- Parallel.ForEach Aggregation ----------------
//            ParallelAggregationWithTpl();

            // -------------- LINQ AsParallel Aggregation ----------------
//            ParallelAggregationWithLinq();
        }

        /// <summary>
        /// The operation we need to run against every line from our history file
        /// </summary>
        static HistoryItem ParseText(string line)
        {
            var results = line.Split(new[] { '|' }, 3);
            Thread.Sleep(1);
            return new HistoryItem
            {
                DateAccessed = Convert.ToDateTime(results[0]),
                Url = results[1],
                Name = results[2]
            };
        }

        private static void SynchronousAggregation()
        {
            using (ExecutionTimer.Start("SynchronousAggregation", blockResults: true))
            {
                var syncResults = new List<HistoryItem>();
                foreach (var line in LoadFilesSync())
                {
                    syncResults.Add(ParseText(line));
                }
                Console.WriteLine("Results: {0}", syncResults.Count);
            }
        }

        private static void ParallelAggregationWithTpl()
        {
            using (ExecutionTimer.Start("Parallel.ForEach", blockResults: true))
            {
                var parallelResults = new List<HistoryItem>();
                var parallelResultsLock = new object();
                Parallel.ForEach(
                    // the IEnumerable to enumerate over...
                    LoadFilesSync(),

                    // initialization
                    () => new List<HistoryItem>(),

                    // loop body
                    (line, loopState, partialResult) =>
                    {
                        partialResult.Add(ParseText(line));
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
                var linqResults = LoadFilesSync().AsParallel().Select(ParseText).ToList();
                Console.WriteLine("Results: {0}", linqResults.Count);
            }
        }

        static readonly string[] FilesToRead = new[] { @"C:\Users\dmohundro\Desktop\chrome-history.txt", @"C:\Users\dmohundro\Desktop\archived-chrome-history.txt" };

        #region Synchronous File Loading
        static IEnumerable<string> LoadFilesSync()
        {
            using (ExecutionTimer.Start("LoadFilesSync", infoOnly: true))
            {
                var lines = new List<string>();
                foreach (var x in FilesToRead)
                {
                    var result = LoadFileSync(x);
                    lines.AddRange(result);
                }
                return lines;
            }
        }

        static IEnumerable<string> LoadFileSync(string filePath)
        {
            using (ExecutionTimer.Start("LoadFileSync", infoOnly: true))
            {
                var lines = new List<string>();
                using (var reader = File.OpenText(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines;
            }
        }
        #endregion

        #region Asynchronous File Loading
        static async Task<IEnumerable<string>> LoadFilesAsync()
        {
            using (ExecutionTimer.Start("LoadFilesAsync", infoOnly: true))
            {
                var lines = new List<string>();
                var linesLock = new object();

                await FilesToRead.ForEachAsync(
                   async x =>
                   {
                       var result = await LoadFileAsync(x);
                       lock (linesLock)
                       {
                           lines.AddRange(result);
                       }
                   });

                return lines;
            }
        }

        async static Task<IEnumerable<string>> LoadFileAsync(string filePath)
        {
            using (ExecutionTimer.Start("LoadFileAsync", infoOnly: true))
            {
                var lines = new List<string>();
                using (var reader = File.OpenText(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines;
            }
        }
        #endregion
    }
}