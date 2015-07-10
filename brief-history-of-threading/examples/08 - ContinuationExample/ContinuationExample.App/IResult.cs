using System;

namespace ContinuationExample
{
    // see https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro/IResult.cs
    // for corresponding implementation in Caliburn
    public interface IResult
    {
        void Execute();
        Action Completed { get; set; }
    }
}