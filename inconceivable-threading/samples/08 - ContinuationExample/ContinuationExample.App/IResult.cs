using System;

namespace ContinuationExample {
    public interface IResult {
        void Execute();
        Action Completed { get; set; }
    }
}