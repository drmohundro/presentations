using System;

namespace ContinuationExample
{
    // see https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro/TaskResult.cs
    // for corresponding implementation Caliburn
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
                    AsyncHelper.BeginOnUIThread(() => _callback(result));
                    AsyncHelper.BeginOnUIThread(() => Completed());
                }, null);
        }

        public Action Completed { get; set; }
    }
}