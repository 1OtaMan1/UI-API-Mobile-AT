using Polly;

namespace Core.Utils;

public static class Retry
{
    public static void Exponential<TException>(int retryCount, Action action)
        where TException : Exception
    {
        Policy.Handle<TException>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .Execute(action);
    }

    public static bool Exponential(int retryCount, Func<bool> function)
    {
        var result = Policy
            .HandleResult<bool>(r => !r)
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAndCapture(function);

        return result.Result;
    }
}