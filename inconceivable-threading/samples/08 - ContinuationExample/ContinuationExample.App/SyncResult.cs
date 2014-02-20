using System;

namespace ContinuationExample {
    public class SyncResult : IResult {
        private readonly Action _action;

        public SyncResult(Action action) {
            _action = action;
            Completed = () => { };
        }

        public void Execute() {
            _action.Invoke();
            Completed();
        }

        public Action Completed { get; set; }
    }
}