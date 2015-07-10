using System;
using System.Threading;

namespace ContinuationExample
{
    internal static class AsyncHelper
    {
        private static SynchronizationContext Context { get; set; }

        public static void InitializeSynchronizationContext(SynchronizationContext context)
        {
            Context = context;
        }

        public static void BeginOnUIThread(Action action)
        {
            Context.Post(o => action(), null);
        }
    }
}