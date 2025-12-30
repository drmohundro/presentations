using System;

namespace ContinuationExample
{
    public class AsyncResult<T> : IResult
    {
        private readonly Func<T> _action;
        private readonly Action<T> _callback;
        private readonly Action<Exception> _errorCallback;

        public AsyncResult(Func<T> action, Action<T> callback, Action<Exception> errorCallback)
        {
            _action = action;
            _callback = callback;
            _errorCallback = errorCallback;
            Completed = () => { };
        }

        public void Execute()
        {
            _action.BeginInvoke(
                iar =>
                {
                    var result = default(T);
                    try
                    {
                        result = _action.EndInvoke(iar);
                    }
                    catch (Exception ex)
                    {
                        _errorCallback(ex);
                        return;
                    }

                    // post the result back on the UI thread
                    Async.BeginOnUIThread(() => _callback(result));
                    Async.BeginOnUIThread(() => Completed());
                }, null);
        }

        public Action Completed { get; set; }
    }
}